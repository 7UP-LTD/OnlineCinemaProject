using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Repositories.IRepositories;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.MovieDtos;
using OnlineCinema.Logic.Response.IResponse;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.Logic.Services
{
    /// <summary>
    /// Реализация сервиса для работы с избранными фильмами пользователя.
    /// </summary>
    public class FavoriteMovieService : IFavoriteMovieService
    {
        private readonly IBaseRepository<UserFavoriteMovieEntity> _favoriteRepository;
        private readonly IBaseRepository<MovieEntity> _movieRepository;
        private readonly IMapper _mapper;
        private readonly IOperationResponse _response;

        /// <summary>
        /// Конструктор класса FavoriteMovieService.
        /// </summary>
        /// <param name="favoriteRepository">Репозиторий избранных фильмов пользователей.</param>
        /// <param name="movieRepository">Репозиторий фильмов.</param>
        /// <param name="response">Сервис для формирования ответов операций.</param>
        public FavoriteMovieService(
            IBaseRepository<UserFavoriteMovieEntity> favoriteRepository,
            IBaseRepository<MovieEntity> movieRepository,
            IMapper mapper,
            IOperationResponse response)
        {
            _favoriteRepository = favoriteRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
            _response = response;
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> AddToFavoritesAsync(Guid userId, Guid movieId)
        {
            var movie = await _movieRepository.GetOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
                return _response.NotFound($"Фильм не найден с таким ID {movieId}.");

            var userFavoriteMovie = new UserFavoriteMovieEntity
            {
                UserId = userId,
                MovieId = movieId
            };
            await _favoriteRepository.AddAsync(userFavoriteMovie);
            return _response.CreatedSuccessfully(userFavoriteMovie.Id);
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> DeleteFromFavoritesAsync(Guid userId, Guid movieId)
        {
            var userFavoriteMovie = await _favoriteRepository.GetOrDefaultAsync(f => f.MovieId == movieId &&
                                                                                     f.UserId == userId);
            if (userFavoriteMovie is null)
                return _response.BadRequest("Неудалось удалить из избранного.");

            await _favoriteRepository.DeleteAsync(userFavoriteMovie);
            return _response.DeleteSuccessfully();
        }

        public async Task<ResponseDto> GetAllUserFavoriteMoviesAsync(Guid userId)
        {
            var favoriteMovies = await _favoriteRepository.GetAllAsync(f => f.UserId == userId, 
                                                                       includeProperty: "MovieEntity");
            if (favoriteMovies is null)
                return _response.NotFound("Избранные фильмы у пользователя не найдены.");

            var movies = favoriteMovies.Select(favoriteMovies => favoriteMovies.Movie).ToList();
            var favoriteMovieDtos = _mapper.Map<List<FavoriteMovieDto>>(movies);
            return _response.SuccessResponse(favoriteMovieDtos);
        }
    }
}
