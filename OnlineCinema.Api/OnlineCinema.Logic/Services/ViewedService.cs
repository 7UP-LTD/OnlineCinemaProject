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
    /// Сервис для работы с просмотрами фильмов пользователей.
    /// </summary>

    public class ViewedService : IViewedService
    {
        private readonly IBaseRepository<UserMovieViewedEntity> _userViewedRepository;
        private readonly IBaseRepository<MovieEntity> _movieRepository;
        private readonly IOperationResponse _response;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса просмотров фильмов пользователей.
        /// </summary>
        /// <param name="userViewedRepository">Репозиторий просмотров фильмов пользователей.</param>
        /// <param name="movieRepository">Репозиторий фильмов.</param>
        /// <param name="response">Сервис для формирования ответов на операции.</param>
        /// <param name="mapper">Маппер для преобразования сущностей.</param>
        public ViewedService(
            IBaseRepository<UserMovieViewedEntity> userViewedRepository,
            IBaseRepository<MovieEntity> movieRepository,
            IOperationResponse response,
            IMapper mapper)
        {
            _userViewedRepository = userViewedRepository;
            _movieRepository = movieRepository;
            _response = response;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> AddUserViewedWatchedTimeAsync(Guid userId, Guid movieId, int watchedTime)
        {
            var movie = await _movieRepository.GetOrDefaultAsync(m => m.Id == movieId);
            if (movie is null)
                return _response.BadRequest("Фильм не найден.");

            var viewedExist = await _userViewedRepository.GetOrDefaultAsync(v => v.UserId == userId &&
                                                                                 v.MovieId == movieId);
            if (viewedExist is not null)
            {
                viewedExist.WatchedTime = watchedTime;
                await _userViewedRepository.UpdateAsync(viewedExist);
                return _response.SuccessResponse(viewedExist.Id);
            }

            var viewed = new UserMovieViewedEntity
            {
                UserId = userId,
                MovieId = movieId,
                WatchedTime = watchedTime,
                CreatedDate = DateTime.Now,
            };

            await _userViewedRepository.AddAsync(viewed);
            return _response.SuccessResponse(viewed.Id);
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> GetAllViewedMoviesOfUserAsync(Guid userId, int currentPage, int pageSize)
        {
            var viewedMovies = await _movieRepository.GetPageEntitiesAsync(m => m.UserMoviesViewed.All(v => v.UserId == userId),
                                                                           currentPage: currentPage,
                                                                           tEntityPerPage: pageSize,
                                                                           includeProperty: "UserMoviesViewed");
            var pageDto = new PageDto<ShortInfoMovieDto>
            {
                CurrentPage = currentPage,
                ItemsPerPage = pageSize,
                TotalItems = viewedMovies.TotalTEntity,
                Items = _mapper.Map<IEnumerable<ShortInfoMovieDto>>(viewedMovies.TEntities)
            };

            return _response.SuccessResponse(pageDto);
        }
    }
}
