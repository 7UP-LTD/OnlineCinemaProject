using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Data.Entities;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с просмотренными фильмами пользователя.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ViewedController : ControllerBase
    {
        private readonly IViewedService _viewedService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<UserMovieViewedEntity> _logger;

        /// <summary>
        /// Конструктор контроллера ViewedController.
        /// </summary>
        /// <param name="viewedService">Сервис для работы с просмотренными фильмами.</param>
        /// <param name="userManager">Менеджер пользователей.</param>
        /// <param name="logger">Логгер для сущности UserMovieViewedEntity.</param>
        public ViewedController(
            IViewedService viewedService,
            UserManager<UserEntity> userManager,
            ILogger<UserMovieViewedEntity> logger)
        {
            _viewedService = viewedService;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// Получает список просмотренных фильмов пользователя.
        /// </summary>
        /// <param name="currentPage">Текущая страница (по умолчанию: 1).</param>
        /// <param name="moviesPerPage">Количество фильмов на странице (по умолчанию: 50).</param>
        /// <returns>Список просмотренных фильмов пользователя.</returns>
        /// <response code="200">Страница со списком просмотренных фильмов пользователя.</response>
        /// <response code="404">Пользователь не найдены. Статус 404 с описанием ошибки.</response>
        /// <response code="500">Ошибка на стороне сервера. Статус 500 с описанием ошибки.</response>
        [HttpGet("GetMovies")]
        [ProducesResponseType(typeof(PageDto<ShortInfoMovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUserMovieViewedAsync([FromQuery] int currentPage = 1,
                                                                    [FromQuery] int moviesPerPage = 50)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound("Пользователь не найден.");

                var operationResponse = await _viewedService.GetAllViewedMoviesOfUserAsync(user.Id, currentPage, moviesPerPage);
                return Ok(operationResponse.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении фильмов просмотренных пользователя.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Добавляет информацию о просмотре фильма пользователем.
        /// </summary>
        /// <param name="movieId">Идентификатор фильма.</param>
        /// <param name="watchedTime">Время просмотра фильма (в минутах).</param>
        /// <returns>Идентификатор добавленной записи о просмотре фильма.</returns>
        /// <response code="200">Фильм успешно добавлен в просмотренные пользователя. Ответ со статусом 200 и ID из списка просмотренных.</response>
        /// <response code="404">Пользователь или фильм не найдены. Статус 404 с описанием ошибки.</response>
        /// <response code="500">Ошибка на стороне сервера. Статус 500 с описанием ошибки.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUserViewedAsync([FromQuery] Guid movieId,
                                                            [FromQuery] int watchedTime)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound("Пользователь не найден.");

                var operationResponse = await _viewedService.AddUserViewedWatchedTimeAsync(user.Id, movieId, watchedTime);
                if (operationResponse.IsSuccess)
                    return Ok(operationResponse.Result);

                return NotFound(operationResponse.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении в просмотренные фильмы пользователем.", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
