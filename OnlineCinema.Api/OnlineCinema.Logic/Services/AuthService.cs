using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineCinema.Data.Entities;
using OnlineCinema.Logic.Dtos.AuthDtos;
using OnlineCinema.Logic.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineCinema.Logic.Services
{
    /// <summary>
    /// Сервис аутентификации и авторизации.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMessageService _message;
        private readonly IEmailSender _emailService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса аутентификации и авторизации.
        /// </summary>
        /// <param name="userManager">Менеджер пользователей.</param>
        /// <param name="mapper">Маппер.</param>
        /// <param name="configuration">Конфигурация.</param>
        public AuthService(
            UserManager<UserEntity> userManager, 
            IMapper mapper, 
            IConfiguration configuration, 
            IMessageService message, 
            IEmailSender emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _message = message;
            _emailService = emailService;
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return new UserManagerResponse
                {
                    Message = "Пользователь не найден"
                };

            var tokenDecoded = WebEncoders.Base64UrlDecode(token);
            var normToken = Encoding.UTF8.GetString(tokenDecoded);
            var result = await _userManager.ConfirmEmailAsync(user, normToken);
            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Электронная почта подтверждена.",
                    IsSuccess = true
                };

            return new UserManagerResponse
            {
                Message = "Почта не подтверждена.",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        /// <inheritdoc/>
        public async Task<UserManagerResponse> ForgetPasswordAsync(string email, string redirectUrl)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return new UserManagerResponse
                {
                    Message = "Не удалось найти пользователя.",
                    Errors= new List<string> {$"Пользователь с такой электронной почтой {email} не найден."}
                };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            var resetLink = $"{_configuration["AppUrl"]}/{redirectUrl}?email={email}&token={validToken}";
            var message = await _message.GetResetEmailHtmlAsync(resetLink);
            await _emailService.SendEmailAsync(email, message.Subject, message.HtmlMessage);
            return new UserManagerResponse
            {
                Message = "Письмо для сброса пароля было успешно отправлено.",
                IsSuccess = true
            };
        }

        /// <inheritdoc/>
        public async Task<UserManagerResponse> LoginUserAsync(LoginUserDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is null)
                return new UserManagerResponse
                {
                    Message = "Пользователь с таким именем пользователя не найден."
                };

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return new UserManagerResponse
                {
                    Message = "Вход запрещён",
                    Errors = new List<string> { "Неверный пароль." }
                };

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true
            };
        }

        /// <inheritdoc/>
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterUserDto model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model), "Модель для регистрации пуста (null).");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Пароли не совпадают.",
                    Errors = new List<string> { "Подтверждения пароля не совпадает с паролем." }
                };

            var userEntity = _mapper.Map<UserEntity>(model);
            var userName = await _userManager.FindByNameAsync(userEntity.UserName!);
            if (userName is not null)
                return new UserManagerResponse
                {
                    Message = "Пользователь уже существует.",
                    Errors = new List<string> { $"Пользователь с таким именем пользователя уже существует {userEntity.UserName}." }
                };

            var result = await _userManager.CreateAsync(userEntity, model.Password);
            if (result.Succeeded)
            {
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(userEntity);
                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
                var confirmationLink = $"{_configuration["AppUrl"]}/api/auth/confirmemail?userid={userEntity.Id}&token={validEmailToken}&redirecturl={model.ConfirmRedirectUrl}";
                var message = await _message.GetConfirmationEmailHtmlAsync(confirmationLink);
                await _emailService.SendEmailAsync(userEntity.Email!, message.Subject, message.HtmlMessage);
                return new UserManagerResponse
                {
                    Message = "Пользователь успешно зарегистирован.",
                    IsSuccess = true
                };
            }

            if (result.Errors.Where(e => e.Code == "DublicateEmail") is not null)
                return new UserManagerResponse
                {
                    Message = "Пользователь не зарегистрирован.",
                    Errors = new List<string> { $"Пользователь с электронной почтой {userEntity.Email} уже существует." }
                };

            return new UserManagerResponse
            {
                Message = "Пользователь не зарегистрирован.",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        /// <inheritdoc/>
        public async Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return new UserManagerResponse
                {
                    Message = "Пользователь не найден",
                    Errors = new List<string> { $"Пользователь с электронной почтой {model.Email} не найден." }
                };

            if (model.NewPassword != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Пароли не совпадают",
                    Errors = new List<string> { "Пароль и подтверждения пароля не совпадают." }
                };

            var tokenDecoded = WebEncoders.Base64UrlDecode(model.Token);
            var normToken = Encoding.UTF8.GetString(tokenDecoded);
            var result = await _userManager.ResetPasswordAsync(user, normToken, model.NewPassword);
            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Пароль был успешно сброшен.",
                    IsSuccess = true
                };

            return new UserManagerResponse
            {
                Message = "Неудалось сбросить пароль",
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}
