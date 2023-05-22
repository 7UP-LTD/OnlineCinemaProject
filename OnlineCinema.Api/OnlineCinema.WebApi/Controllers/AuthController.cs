using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.AuthDtos;
using OnlineCinema.Logic.Response.IResponse;
using OnlineCinema.Logic.Services.IServices;
using OnlineCinema.Logic.Response;
using Microsoft.AspNetCore.Identity;
using OnlineCinema.Data.Entities;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace OnlineCinema.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для аунтентификации.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// Конструктор контроллера аутентификации.
        /// </summary>
        /// <param name="authService">Сервис аутентификации.</param>
        /// <param name="configuration">Конфигурация.</param>
        ///  <param name="logger">Журнал логирования.</param>
        public AuthController(
            IAuthService authService, 
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Метод для внешней аутентификации пользователя через Google.
        /// </summary>
        /// <remarks>
        /// Метод перенаправляет пользователя в Google. Для теста вводите адрес метода в адресную строку.
        /// После умпешной внешней аутентификации выдает токен доступа JWT Bearer.
        /// </remarks>
        /// <returns>Возврат токена в случае успешной аутентификации.</returns>
        /// <response code="200">Успешный вход получение токена доступа пользователя.</response>
        /// <response code="400">Неправильный запрос для регистрации.</response>
        /// <response code="500">Ошибка на стороне севрера.</response>
        [HttpGet("GoogleLogin")]
        [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GoogleExternalLoginAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
                return BadRequest("Не удалось войти.");

            UserManagerDto? result;
            if (!_authService.IsUserExistFindByEmail(email!).GetAwaiter().GetResult())
            {
                result = await _authService.GoogleExternalLoginRegisterAsync(User!);
                return Ok(result.Message);
            }

            result = await _authService.GoogleExternalLoginAsync(email);
            if (result.IsSuccess)
                return Ok(result.Message);

            return BadRequest(string.Join(',', result.Errors!));
        }

        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        /// <param name="model">DTO регистрируемого пользователя.</param>
        /// <returns>Ответ об успешности регистрации пользователя.</returns>
        /// <response code="200">Пользователь зарегестрирован.</response>
        /// <response code="400">Неправильный запрос для регистрации.</response>
        /// <response code="500">Ошибка на стороне севрера.</response>
        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserManagerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserManagerDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new UserManagerDto
                    {
                        Message = "Одно или несколько полей не валидны.",
                        Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage).ToList())
                    });

                var result = await _authService.RegisterUserAsync(model);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при регистрации пользователя.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Подтверждение адреса электронной почты пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="token">Токен подтверждения.</param>
        /// <param name="redirectUrl">URL для перенаправления после подтверждения.</param>
        /// <returns>Результат подтверждения адреса электронной почты.</returns>
        /// <response code="200">Адрес электронной почты успешно подтвержден.</response>
        /// <response code="404">Пользователь или токен не найдены.</response>
        /// <response code="400">Неправильный запрос для подтверждения адреса электронной почты.</response>
        /// <response code="500">Ошибка на стороне сервера.</response>
        [HttpGet("ConfirmEmail")]
        [ProducesResponseType(typeof(UserManagerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserManagerDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAsync(string userId, string token, string redirectUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                    return NotFound();

                var result = await _authService.ConfirmEmailAsync(userId, token);
                if (result.IsSuccess)
                    return Redirect($"{_configuration["AppUrl"]}/{redirectUrl}");

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при подтверждении почты пользователя.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Аутентификация пользователя.
        /// </summary>
        /// <param name="model">DTO для входа пользователя.</param>
        /// <returns>Ответ менеджера пользователя.</returns>
        /// <response code="200">Пользователь зарегестрирован.</response>
        /// <response code="400">Неправильный запрос для регистрации.</response>
        /// <response code="500">Ошибка на стороне севрера.</response>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserManagerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserManagerDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new UserManagerDto
                    {
                        Message = "Одно или несколько полей не валидны.",
                        Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage).ToList())
                    });

                var result = await _authService.LoginUserAsync(model);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при входе в учетную запись пользователя.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Запрос на сброс пароля пользователя.
        /// </summary>
        /// <param name="email">Email пользователя.</param>
        /// <param name="redirectUrl">URL для перенаправления для сброса пароля.</param>
        /// <returns>Ответ менеджера пользователя.</returns>
        /// <response code="200">Запрос на сброс пароля успешно выполнен.</response>
        /// <response code="404">Пользователь не найден.</response>
        /// <response code="400">Неправильный запрос для сброса пароля.</response>
        /// <response code="500">Ошибка на стороне сервера.</response>
        [HttpPost("ForegetPassword")]
        [ProducesResponseType(typeof(UserManagerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserManagerDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(string email, string redirectUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return NotFound();

                var result = await _authService.ForgetPasswordAsync(email, redirectUrl);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при запросе на сброс пароля пользователя.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Сброс пароля пользователя.
        /// </summary>
        /// <param name="model">DTO для сброса пароля.</param>
        /// <returns>Ответ менеджера пользователя.</returns>
        /// <response code="200">Пароль успешно сброшен.</response>
        /// <response code="400">Неправильный запрос для сброса пароля.</response>
        /// <response code="500">Ошибка на стороне сервера.</response>
        [HttpPost("ResetPassword")]
        [ProducesResponseType(typeof(UserManagerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserManagerDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new UserManagerDto
                    {
                        Message = "Одно или несколько полей не валидны.",
                        Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage).ToList())
                    });

                var result = await _authService.ResetPasswordAsync(model);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Ошибка при смене пароля пользователя.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
