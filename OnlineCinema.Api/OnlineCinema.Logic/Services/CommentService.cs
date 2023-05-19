using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Repositories;
using OnlineCinema.Data.Repositories.IRepositories;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.CommentDto;
using OnlineCinema.Logic.Dtos.CommentDtos;
using OnlineCinema.Logic.Response.IResponse;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.Logic.Services
{
    /// <summary>
    /// Сервис для управления комментариями.
    /// </summary>
    public class CommentService : ICommentService
    {
        private readonly IBaseRepository<MovieCommentEntity> _commentRepository;
        private readonly IBaseRepository<MovieEntity> _movieRepository;
        private readonly IMapper _mapper;
        private readonly IOperationResponse _response;

        /// <summary>
        /// Инициализирует новый экземпляр класса CommentService.
        /// </summary>
        /// <param name="commentRepository">Репозиторий для комментариев к фильмам.</param>
        /// <param name="response">Сервис для формирования ответов операций.</param>
        /// <param name="userRepository">Репозиторий пользователей.</param>
        /// <param name="movieRepository">Репозиторий фильмов.</param>
        /// <param name="mapper">Маппер для преобразования моделей.</param>
        public CommentService(
            BaseRepository<MovieCommentEntity> commentRepository,
            IOperationResponse response,
            IBaseRepository<MovieEntity> movieRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _movieRepository = movieRepository;
            _response = response;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> PostNewCommentAsync(Guid userId, NewCommentDto model)
        {
            var doesCommentExist = _commentRepository.GetOrDefaultAsync(c => c.MovieId == model.MovieId &&
                                                                             c.UserId == userId &&
                                                                             c.Text == model.Text);
            if (doesCommentExist is not null)
                return _response.BadRequest("Комментарий уже существует.");

            var movie = await _movieRepository.GetOrDefaultAsync(u => u.Id == model.MovieId);
            if (movie is null)
                return _response.NotFound($"Фмльм по указанному ID {model.MovieId} не найден.");

            var newComment = _mapper.Map<MovieCommentEntity>(model);
            newComment.UserId = userId;
            await _commentRepository.AddAsync(newComment);
            return _response.SuccessResponse(newComment.Id);
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> UpdateCommentAsync(UpdateCommnetDto model)
        {
            var comment = await _commentRepository.GetOrDefaultAsync(c => c.Id == model.Id);
            if (comment is null)
                return _response.NotFound($"Комментарий по указанному ID {model.Id} не найден.");

            comment.Text = model.Text;
            return _response.SuccessResponse();
        }

        /// <inheritdoc/>
        public async Task<ResponseDto> DeleteCommentAsync(Guid commentId)
        {
            var comment = await _commentRepository.GetOrDefaultAsync(c => c.Id == commentId);
            if (comment is null)
                return _response.NotFound($"Комментарий по указанному ID {commentId} не найден.");

            await _commentRepository.DeleteAsync(comment);
            return _response.DeleteSuccessfully();
        }
    }
}
