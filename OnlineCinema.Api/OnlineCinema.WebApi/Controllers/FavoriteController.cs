using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Data.Entities;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Services.IServices;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineCinema.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с избранными фильмами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteMovieService _favoriteMovieService;
        private readonly UserManager<UserEntity> _userManager;

        /// <summary>
        /// Конструктор класса FavoriteController.
        /// </summary>
        /// <param name="favoriteMovieService">Сервис для работы с избранными фильмами.</param>
        /// <param name="userManager">Менеджер пользователей для получения текущего пользователя.</param>
        public FavoriteController(IFavoriteMovieService favoriteMovieService, UserManager<UserEntity> userManager)
        {
            _favoriteMovieService = favoriteMovieService;
            _userManager = userManager;
        }

        /// <summary>
        /// Получить список всех избранных фильмов пользователя.
        /// </summary>
        /// <returns>Список избранных фильмов пользователя.</returns>
        /// <response code="200">Успешный ответ со статусом 200 и списком избранных фильмов пользователя.</response>
        /// <response code="404">Пользователь не найден статус 404.</response>
        /// <response code="400">Плохой запрос ответ со статусом 400 и списком ошибок.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<FavoriteMovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFavoriteMoviesAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
                return NotFound("Пользователя не найден.");

            var operationResult = await _favoriteMovieService.GetAllUserFavoriteMoviesAsync(user.Id);
            if (operationResult.IsSuccess)
                return Ok(operationResult.Result);

            return BadRequest(operationResult.Errors);
        }

        /// <summary>
        /// Добавить фильм в избранное пользователя.
        /// </summary>
        /// <returns> ID созданого добавления.</returns>
        /// <response code="200">Успешный ответ со статусом 200 и фильм добавлен и ID созданого добавления.</response>
        /// <response code="404">Пользователь не найден статус 404.</response>
        /// <response code="400">Плохой запрос ответ со статусом 400 и списком ошибок.</response>
        /// <response code="500">Ошибка на сервере статус 500 с описанием ошибки.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddToFavoritesAsync(Guid movieId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user is null)
                    return NotFound("Пользователя не найден.");

                var operationResult = await _favoriteMovieService.AddToFavoritesAsync(user.Id, movieId);
                if (operationResult.IsSuccess)
                    return Ok(operationResult.Result);

                return BadRequest(operationResult.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Удалить фильм из избранного пользователя.
        /// </summary>
        /// <returns>Ответ со статусом операции.</returns>
        /// <response code="204">Успешный ответ со статусом 204 и фильм удален из избранного.</response>
        /// <response code="404">Пользователь не найден статус 404.</response>
        /// <response code="400">Плохой запрос ответ со статусом 400 и списком ошибок.</response>
        /// <response code="500">Ошибка на сервере статус 500 с описанием ошибки.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFromFavoritesAsync(Guid movieId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user is null)
                    return NotFound("Пользователя не найден.");

                var operationResult = await _favoriteMovieService.DeleteFromFavoritesAsync(user.Id, movieId);
                if (operationResult.IsSuccess)
                    return NoContent();

                return BadRequest(operationResult.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
