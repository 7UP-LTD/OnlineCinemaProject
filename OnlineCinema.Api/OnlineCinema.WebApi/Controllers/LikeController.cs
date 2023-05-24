using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Repositories;
using OnlineCinema.Logic;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.MovieDtos;
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
        private readonly ILogger<LikeController> _logger;

        /// <summary>
        /// Конструктор контроллера лайков к фильмам.
        /// </summary>
        /// <param name="likeService">Сервис для работы с лайками.</param>
        /// <param name="userManager">Менеджер пользователей.</param>
        /// <param name="logger">Журнал логирования.</param>
        public LikeController(
            ILikeService likeService, 
            UserManager<UserEntity> userManager, 
            ILogger<LikeController> logger)
        {
            _likeService = likeService;
            _userManager = userManager;
            _logger = logger;

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
        public async Task<IActionResult> LikeMovieAsync(Guid movieId)
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
                _logger.LogError(ex, "Ошибка при добавлении лайка на фильм пользователем.", ex.Message);
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
                _logger.LogError(ex, "Ошибка при добавлении дизлайка на фильм пользователем.", ex.Message);
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
        [HttpDelete("Delete/{movieId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLikeAsync(Guid movieId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user is null)
                    return NotFound("Пользователь не найден.");

                var operationResponse = await _likeService.DeleteLikeAsync(movieId, user.Id);
                if (operationResponse.IsSuccess)
                    return NoContent();

                return NotFound(operationResponse.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении лайка/дизлайка с фильма пользователем.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Получает список фильмов, которые понравились пользователю.
        /// </summary>
        /// <param name="currentPage">Текущая страница.</param>
        /// <param name="moviesPerPage">Количество фильмов на странице.</param>
        /// <returns>Информация о фильмах с пагинацией.</returns>
        /// <response code="200">Информация о фильмах с пагинацией.</response>
        /// <response code="404">Пользователь не найдены.</response>
        /// <response code="500">Ошибка на сервере с кодам 500 и описанием ошибки.</response>
        [HttpGet("GetMovies")]
        [ProducesResponseType(typeof(PageDto<ShortInfoMovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUserMovieLikesAsync([FromQuery] int currentPage = 1, 
                                                                   [FromQuery] int moviesPerPage = 50)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound("Пользователь не найден.");

                var operationResponse = await _likeService.GetUserLikeMoviesAsync(user.Id, currentPage, moviesPerPage);
                return Ok(operationResponse.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении фильмов с лайком на фильмы пользователя.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
