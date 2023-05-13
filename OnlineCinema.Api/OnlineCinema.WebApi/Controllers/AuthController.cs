using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.AuthDtos;
using OnlineCinema.Logic.Services.IServices;
using System.Net;

namespace OnlineCinema.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для аунтентификации.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Конструктор контроллера аутентификации.
        /// </summary>
        /// <param name="authService">Сервис аутентификации.</param>
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
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
        [ProducesResponseType(typeof(UserManagerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserManagerResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new UserManagerResponse
                    {
                        Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage).ToList())
                    });

                var result = await _authService.RegisterUserAsync(model);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                //TODO: Добавить жернал логгирования
                var errorModel = new ErrorResponse
                {
                    ErrorMessage = "Произошла ошибка при выполнении операции.",
                    StatusCode = HttpStatusCode.InternalServerError,
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserManagerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserManagerResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new UserManagerResponse
                    {
                        Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage).ToList())
                    });

                var result = await _authService.LoginUserAsync(model);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                //TODO: Добавить жернал логгирования
                var errorModel = new ErrorResponse
                {
                    ErrorMessage = "Произошла ошибка при выполнении операции.",
                    StatusCode = HttpStatusCode.InternalServerError,
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
            }
        }

        [HttpGet("ConfirmEmail")]
        [ProducesResponseType(typeof(UserManagerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserManagerResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmailAsync(string userId, string token)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                    return NotFound();

                var result = await _authService.ConfirmEmailAsync(userId, token);
                if (result.IsSuccess)
                {
                    return Redirect($"{_configuration["AppUrl"]}/api/test/userconfirmemail?userid={userId}&token={token}");
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                //TODO: Добавить жернал логгирования
                var errorModel = new ErrorResponse
                {
                    ErrorMessage = "Произошла ошибка при выполнении операции.",
                    StatusCode = HttpStatusCode.InternalServerError,
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
            }
        } 
    }
}
