using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Получение списка фильмов 
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Количество фильмов на странице</param>
        /// <param name="filter">Фильтры: по наименованию, по списку тэгов(guid), по списку жанров(guid)</param>
        /// <returns></returns>
        [HttpPost("list")]
        public async Task<IActionResult> GetMovies(int page, int pageSize, MovieFilter filter)
        {
            var moviesList = await _movieService.GetMovies(page, pageSize, filter);
            return Ok(moviesList);
        }

        /// <summary>
        /// Получение фильма по id
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(Guid id)
        {
            var moviesItem = await _movieService.GetMovieById(id);
            return Ok(moviesItem);
        }

        /// <summary>
        /// Добавление нового фильма
        /// </summary>
        /// <param name="movie">DTO фильма</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] ChangeMovieRequest movie)
        {
            //TODO проверка на права по созданию
            await _movieService.CreateMovie(movie);
            return Ok(movie);
        }

        /// <summary>
        /// Редактирование фильма
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        /// <param name="movie">DTO измененного фильма</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(Guid id, [FromBody] ChangeMovieRequest movie)
        {
            await _movieService.UpdateMovie(id, movie);
            return Ok();
        }

        /// <summary>
        /// Удаление фильма по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            var moviesItem = await _movieService.GetMovieById(id);
            if (moviesItem == null)
            {
                return NotFound();
            }

            //TODO проверка на права для удаления

            await _movieService.DeleteMovie(id);
            return Ok();
        }
    }
}