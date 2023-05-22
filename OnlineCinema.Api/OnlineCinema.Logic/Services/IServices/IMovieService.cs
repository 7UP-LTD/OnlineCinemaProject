using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Dtos.MovieDtos.MainPageDtos;
using OnlineCinema.Logic.Models;

namespace OnlineCinema.Logic.Services.IServices
{
    /// <summary>
    /// Интерфейс сервиса для Movie
    /// </summary>
    public interface IMovieService
    {
        /// <summary>
        /// Постраничное получение списка фильмов
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Количество на странице</param>
        /// <param name="filter">Фильтры: по наименование, по списку тэгов(guid), по списку жанров(guid)</param>
        /// <returns>Список фильмов</returns>
        Task<List<MovieDto>> GetMovies(int page, int pageSize, MovieFilter? filter);

        /// <summary>
        /// Получение фильма по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        /// <returns>Фильм DTO</returns>
        Task<MovieDto> GetMovieById(Guid id);

        /// <summary>
        /// Создание фильма
        /// </summary>
        /// <param name="movie">Данные фильма</param>
        /// <returns>Идентификатор созданного фильма</returns>
        Task<Guid> CreateMovie(ChangeMovieRequest movie);

        /// <summary>
        /// Обновление фильма
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        /// <param name="movie">Данные фильма</param>
        Task UpdateMovie(Guid id, ChangeMovieRequest movie);

        /// <summary>
        /// Удаление фильма
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        Task DeleteMovie(Guid id);

        /// <summary>
        /// Получение данных для главной страницы
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<MovieMainView> GetMoviesForMain(Guid? userId);
    }
}