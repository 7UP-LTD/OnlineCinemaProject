using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineCinema.Data.Entities;
using OnlineCinema.Logic.Dtos.AuthDtos;
using OnlineCinema.Logic.Response.IResponse;
using OnlineCinema.Logic.Services.IServices;
using System;
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
        private readonly IUserManagerResponse _managerResponse;
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
            IEmailSender emailService,
            IUserManagerResponse managerResponse)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _message = message;
            _emailService = emailService;
            _managerResponse = managerResponse;
        }

        /// <inheritdoc/>
        public async Task<UserManagerDto> RegisterUserAsync(RegisterUserDto model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model), "Модель для регистрации пуста (null).");

            if (model.Password != model.ConfirmPassword)
                return _managerResponse.InvalidConfirmPassword();

            var userEntity = _mapper.Map<UserEntity>(model);
            var userName = await _userManager.FindByNameAsync(userEntity.UserName!);
            if (userName is not null)
                return _managerResponse.UserNameAlreadyExist(userName.UserName!);

            var result = await _userManager.CreateAsync(userEntity, model.Password);
            if (result.Succeeded)
            {
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(userEntity);
                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
                var confirmationLink = $"{_configuration["AppUrl"]}/api/auth/confirmemail?userid={userEntity.Id}&token={validEmailToken}&redirecturl={model.ConfirmRedirectUrl}";
                var message = await _message.GetConfirmationEmailHtmlAsync(confirmationLink);
                await _emailService.SendEmailAsync(userEntity.Email!, message.Subject, message.HtmlMessage);
                return _managerResponse.UserRegisterSuccessfully();
            }

            if (result.Errors.Where(e => e.Code == "DublicateEmail") is not null)
                return _managerResponse.DublicateEmail(userEntity.Email!);

            return _managerResponse.UserRegisterFailed(result.Errors.Select(e => e.Description).ToList());
        }

        /// <inheritdoc/>
        public async Task<UserManagerDto> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return _managerResponse.NotFound(new List<string> { $"Не найден пользователь с ID: {userId}." });

            if (token is null)
                return _managerResponse.InvalidToken(new List<string> { "Отсутсвует токен." });

            var tokenDecoded = WebEncoders.Base64UrlDecode(token);
            var normToken = Encoding.UTF8.GetString(tokenDecoded);
            var result = await _userManager.ConfirmEmailAsync(user, normToken);
            if (result.Succeeded)
                return _managerResponse.ConfirmEmailSuccessfully();

            return _managerResponse.ConfirmEmailFailed(result.Errors.Select(e => e.Description).ToList());
        }

        /// <inheritdoc/>
        public async Task<UserManagerDto> LoginUserAsync(LoginUserDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is null)
                return _managerResponse.NotFound(new List<string> { $"Пользователь с именем пользователя {model.UserName} не найден." });

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return _managerResponse.EntryDenied(new List<string> { "Неверный пароль." });

            if (!user.EmailConfirmed)
                return _managerResponse.EntryDenied(new List<string> { $"Подтвердите электронную почту пользователя {user.Email} для входа." });

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
            return _managerResponse.EntrySuccessfully(tokenAsString);
        }

        /// <inheritdoc/>
        public async Task<UserManagerDto> ForgetPasswordAsync(string email, string redirectUrl)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return _managerResponse.NotFound(new List<string> { $"Пользователь с такой электронной почтой {email} не найден." });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            var resetLink = $"{_configuration["AppUrl"]}/{redirectUrl}?email={email}&token={validToken}";
            var message = await _message.GetResetEmailHtmlAsync(resetLink);
            await _emailService.SendEmailAsync(email, message.Subject, message.HtmlMessage);
            return _managerResponse.EmailForResetPasswordSentSuccessfully();
        }

        /// <inheritdoc/>
        public async Task<UserManagerDto> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return _managerResponse.NotFound(new List<string> { $"Пользователь с электронной почтой {model.Email} не найден." });

            if (model.NewPassword != model.ConfirmPassword)
                return _managerResponse.InvalidConfirmPassword();

            var tokenDecoded = WebEncoders.Base64UrlDecode(model.Token);
            var normToken = Encoding.UTF8.GetString(tokenDecoded);
            var result = await _userManager.ResetPasswordAsync(user, normToken, model.NewPassword);
            if (result.Succeeded)
                return _managerResponse.ResetPasswordSuccessfully();

            return _managerResponse.ResetPasswordFailed(result.Errors.Select(e => e.Description).ToList());
        }
    }
}
