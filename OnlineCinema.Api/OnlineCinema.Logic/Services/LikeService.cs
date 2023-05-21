﻿using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Repositories.IRepositories;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Response.IResponse;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.Logic.Services
{
    /// <summary>
    /// Сервис для работы с лайками фильмов.
    /// </summary>
    public class LikeService : ILikeService
    {
        private readonly IBaseRepository<UserMovieLikeEntity> _likeRepository;
        private readonly IBaseRepository<MovieEntity> _movieRepository;
        private readonly IOperationResponse _response;

        /// <summary>
        /// Конструктор класса LikeService.
        /// </summary>
        /// <param name="likeRepository">Репозиторий для работы с данными о лайках пользователей.</param>
        /// <param name="response">Сервис для формирования операционных ответов.</param>
        /// <param name="movieRepository">Репозиторий для работы с данными о фильмах.</param>
        public LikeService(
            IBaseRepository<UserMovieLikeEntity> likeRepository,
            IOperationResponse response,
            IBaseRepository<MovieEntity> movieRepository)
        {
            _likeRepository = likeRepository;
            _response = response;
            _movieRepository = movieRepository;
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> DislikeAsync(Guid movieId, Guid userId)
        {
            var userMovieLike = await _likeRepository.GetOrDefaultAsync(l => l.MovieId == movieId &&
                                                                             l.UserId == userId);
            if (userMovieLike is not null)
            {
                if (!userMovieLike.isLike)
                    return _response.SuccessResponse(userMovieLike.Id);

                userMovieLike.isLike = false;
                await _likeRepository.UpdateAsync(userMovieLike);
                return _response.SuccessResponse(userMovieLike.Id);
            }

            var movie = _movieRepository.GetOrDefaultAsync(m => m.Id == movieId);
            if (movie is null)
                return _response.NotFound("Фильм не найден.");

            userMovieLike = new UserMovieLikeEntity
            {
                MovieId = movieId,
                UserId = userId,
                isLike = false
            };

            await _likeRepository.AddAsync(userMovieLike);
            return _response.SuccessResponse(userMovieLike.Id);
        }
        
        /// <inheritdoc/>
        public async Task<ResponseDto> LikeAsync(Guid movieId, Guid userId)
        {
            var userMovieLike = await _likeRepository.GetOrDefaultAsync(l => l.MovieId == movieId &&
                                                                             l.UserId == userId);
            if (userMovieLike is not null)
            {
                if (userMovieLike.isLike)
                    return _response.SuccessResponse(userMovieLike.Id);

                userMovieLike.isLike = true;
                await _likeRepository.UpdateAsync(userMovieLike);
                return _response.SuccessResponse(userMovieLike.Id);
            }

            var movie = _movieRepository.GetOrDefaultAsync(m => m.Id == movieId);
            if (movie is null)
                return _response.NotFound("Фильм не найден.");

            userMovieLike = new UserMovieLikeEntity
            {
                MovieId = movieId,
                UserId = userId,
                isLike = true
            };

            await _likeRepository.AddAsync(userMovieLike);
            return _response.SuccessResponse(userMovieLike.Id);
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> DeleletLikeAsync(Guid movieId, Guid userId)
        {
            var userMovieLike = await _likeRepository.GetOrDefaultAsync(l => l.MovieId == movieId &&
                                                                             l.UserId == userId);
            if (userMovieLike is null)
                return _response.NotFound("Лайка не существует.");

            await _likeRepository.DeleteAsync(userMovieLike);
            return _response.DeleteSuccessfully();
        }
    }
}