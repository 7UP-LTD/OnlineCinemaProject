using System;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.WebApi.Controllers
{
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
        public IActionResult GetMovies(int page, int pageSize, MovieFilter filter)
        {
            var moviesList = _movieService.GetMovies(page, pageSize, filter);
            return Ok(moviesList);
        }

        /// <summary>
        /// Получение фильма по id
        /// </summary>
        /// <param name="id">Id фильма</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetMovieById(Guid id)
        {
            var moviesItem = _movieService.GetMovieById(id);
            return Ok(moviesItem);
        }

        /// <summary>
        /// Добавление нового фильма
        /// </summary>
        /// <param name="movie">DTO фильма</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateMovie([FromBody] ChangeMovieRequest movie)
        {
            //TODO проверка на права по созданию
            _movieService.CreateMovie(movie);
            return Ok(movie);
        }

        /// <summary>
        /// Удаление фильма по id
        /// </summary>
        /// <param name="id">Id фильма</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteHabit(Guid id)
        {
            var moviesItem = _movieService.GetMovieById(id);
            if (moviesItem == null)
            {
                return NotFound();
            }

            //TODO проверка на права для удаления

            _movieService.DeleteMovie(id);
            return Ok();
        }
    }
}