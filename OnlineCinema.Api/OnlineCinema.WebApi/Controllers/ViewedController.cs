using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineCinema.Data.Entities;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Services;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ViewedController : ControllerBase
    {
        private readonly IViewedService _viewedService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<UserMovieViewedEntity> _logger;

        public ViewedController(
            IViewedService viewedService,
            UserManager<UserEntity> userManager,
            ILogger<UserMovieViewedEntity> logger)
        {
            _viewedService = viewedService;
            _userManager = userManager;
            _logger = logger;
        }

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

        [HttpGet("{movieId}")]
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
