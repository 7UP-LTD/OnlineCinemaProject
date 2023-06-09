﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Filters;

namespace OnlineCinema.Data.Repositories.IRepositories
{
    public interface IMovieRepository : IBaseRepository<MovieEntity>
    {
        Task<MovieEntity?> GetMovieById(Guid movieId);

        Task<List<MovieEntity>> GetPagedMovies(int page, int pageSize, MovieEntityFilter? filter);
        Task<List<Guid>> GetTopGenres(int topRows);
        Task<List<MovieEntity>> GetTopMovies(int topRows, Guid? genreId);
        Task<List<MovieEntity>> GetTopUserMovies(Guid? userId, int topRows);
    }
}