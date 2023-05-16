using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.MovieDtos;
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
        /// <returns></returns>
        Task<List<MovieDto>> GetMovies(int page, int pageSize, MovieFilter? filter);
        Task<MovieDto> GetMovieById(Guid id);
        Task<Guid> CreateMovie(ChangeMovieRequest movie);
        Task UpdateMovie(Guid id, ChangeMovieRequest movie);
        Task DeleteMovie(Guid id);
    
        

    }
}