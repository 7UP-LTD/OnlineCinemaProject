using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Logic.Dtos;
using System.Security.Claims;

namespace OnlineCinema.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для теста.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Проверка получения инфы для авторизованных пользователей.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = user.Value;
            return Ok(new List<TestUser>
            {
                new TestUser {FirstName = "John", LastName = "Smith"},
                new TestUser {FirstName = "Amely", LastName = "Blant"}
            });
        }

        /// <summary>
        /// Проверка ссылки для подтверждения электронной почты.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        /// <param name="token">Токен для подтверждения пароля.</param>
        /// <returns></returns>
        [HttpGet("UserConfirmEmail")]
        [AllowAnonymous]
        public IActionResult UserConfirmEmail(string userId, string token)
        {
            var userTest = new TestUser { FirstName = userId, LastName = token };
            return Ok(userTest);
        }

        [HttpGet("ForegetPassword")]
        [AllowAnonymous]
        public IActionResult ForegetPassword(string email, string token)
        {
            return Ok($"{email}   {token}");
        }
    }
}
