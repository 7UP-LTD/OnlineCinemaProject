using OnlineCinema.Logic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Logic.Services.IServices
{
    /// <summary>
    /// Интерфейс сервиса для работы с лайками фильмов.
    /// </summary>
    public interface ILikeService
    {
        /// <summary>
        /// Поставить лайк фильму.
        /// </summary>
        /// <param name="movieId">Идентификатор фильма.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Объект <see cref="ResponseDto"/> с результатом операции.</returns>
        Task<ResponseDto> LikeAsync(Guid movieId, Guid userId);

        /// <summary>
        /// Убрать лайк с фильма.
        /// </summary>
        /// <param name="movieId">Идентификатор фильма.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Объект <see cref="ResponseDto"/> с результатом операции.</returns>
        Task<ResponseDto> DislikeAsync(Guid movieId, Guid userId);

        /// <summary>
        /// Удалить лайк с фильма.
        /// </summary>
        /// <param name="movieId">Идентификатор фильма.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Объект <see cref="ResponseDto"/> с результатом операции.</returns>
        Task<ResponseDto> DeleletLikeAsync(Guid movieId, Guid userId);

        /// <summary>
        /// Асинхронно получает список фильмов, которые понравились пользователю, с использованием пагинации.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="currentPage">Текущая страница.</param>
        /// <param name="moviesPerPage">Количество фильмов на странице.</param>
        /// <returns>Объект <see cref="ResponseDto"/> с результатом запроса.</returns>
        Task<ResponseDto> GetUserLikeMoviesAsync(Guid userId, int currentPage, int moviesPerPage);
    }
}
