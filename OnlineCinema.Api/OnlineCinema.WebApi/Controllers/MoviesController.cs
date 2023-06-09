﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Data.Entities;
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
        private readonly UserManager<UserEntity> _userManager;

        public MoviesController(IMovieService movieService, UserManager<UserEntity> userManager)
        {
            _movieService = movieService;
            _userManager = userManager;
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
        /// Получение данных для главной страницы
        /// </summary>
        /// <returns></returns>
        [HttpPost("main")]
        public async Task<IActionResult> GetMoviesMain()
        {
            var user = await _userManager.GetUserAsync(User);
            Guid? userId = null;
            if (user != null)
            {
                userId = user.Id;
            }

            var moviesList = await _movieService.GetMoviesForMain(userId);
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
        [DisableRequestSizeLimit]
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromForm] ChangeMovieRequest movie)
        {
            var guid = await _movieService.CreateMovie(movie);
            return Ok(guid);
        }

        /// <summary>
        /// Редактирование фильма
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        /// <param name="movie">DTO измененного фильма</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(Guid id, [FromForm] ChangeMovieRequest movie)
        {
            try
            {
                await _movieService.UpdateMovie(id, movie);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
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

            await _movieService.DeleteMovie(id);
            return Ok();
        }
    }
}