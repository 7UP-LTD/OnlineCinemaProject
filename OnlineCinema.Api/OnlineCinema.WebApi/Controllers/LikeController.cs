using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Data.Entities;
using OnlineCinema.Logic.Response.IResponse;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с лайками к фильмам.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        private readonly UserManager<UserEntity> _userManager;

        /// <summary>
        /// Конструктор контроллера лайков к фильмам.
        /// </summary>
        /// <param name="likeService">Сервис для работы с лайками.</param>
        /// <param name="userManager">Менеджер пользователей.</param>
        public LikeController(ILikeService likeService, UserManager<UserEntity> userManager)
        {
            _likeService = likeService;
            _userManager = userManager;
        }

        /// <summary>
        /// Добавить лайк к фильму.
        /// </summary>
        /// <param name="movieId">Идентификатор фильма.</param>
        /// <returns>Объект, представляющий результат операции.</returns>
        /// <response code="200">Лайк добавлен. Ответ со статусом 200 и с ID лайка.</response>
        /// <response code="404">Пользователь или фильм не найдены.</response>
        /// <response code="500">Ошибка на сервере с кодам 500 и описанием ошибки.</response>
        [HttpGet("{movieId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LikeMovieAsync([FromQuery] Guid movieId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound("Пользователь не найден.");

                var operationResponse = await _likeService.LikeAsync(movieId, user.Id);
                if (operationResponse.IsSuccess)
                    return Ok(operationResponse.Result);

                return NotFound(operationResponse.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Добавить дизлайк к фильму.
        /// </summary>
        /// <param name="movieId">Идентификатор фильма.</param>
        /// <returns>Объект, представляющий результат операции.</returns>
        /// <response code="200">Дизлайк добавлен. Ответ со статусом 200 и ID дизлайка.</response>
        /// <response code="404">Пользователь или фильм не найдены.</response>
        /// <response code="500">Ошибка на сервере с кодам 500 и описанием ошибки.</response>
        [HttpGet("Dislike/{movieId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DislikeMovieAsync(Guid movieId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound("Пользователь не найден.");

                var operationResponse = await _likeService.DislikeAsync(movieId, user.Id);
                if (operationResponse.IsSuccess)
                    return Ok(operationResponse.Result);

                return NotFound(operationResponse.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Удалить любую рекцию на фильм лайк или дизлайк с фильма.
        /// </summary>
        /// <param name="movieId">Идентификатор фильма.</param>
        /// <returns>Объект, представляющий результат операции.</returns>
        /// <response code="204">Лайк удален.</response>
        /// <response code="404">Пользователь или лайк не найдены.</response>
        /// <response code="500">Ошибка на сервере с кодам 500 и описанием ошибки.</response>
        [HttpGet("Delete/{movieId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLikeAsync(Guid movieId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound("Пользователь не найден.");

                var operationResponse = await _likeService.DeleletLikeAsync(movieId, user.Id);
                if (operationResponse.IsSuccess)
                    return NoContent();

                return NotFound(operationResponse.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
