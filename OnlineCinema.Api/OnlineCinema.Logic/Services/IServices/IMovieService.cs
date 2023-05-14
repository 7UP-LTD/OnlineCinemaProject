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
        Task<List<MovieDto>> GetMovies(int page, int pageSize, MovieFilter? filter);
        Task<MovieDto> GetMovieById(Guid id);
        Guid CreateMovie(ChangeMovieRequest movie, Guid userId);
        void UpdateMovie(Guid id, ChangeMovieRequest movie);
        void DeleteMovie(Guid id);
    
        

    }
}