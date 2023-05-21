using OnlineCinema.Logic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Logic.Services.IServices
{
    /// <summary>
    /// Интерфейс сервиса для работы с просмотрами пользователей.
    /// </summary>
    public interface IViewedService
    {
        /// <summary>
        /// Метод для добавления информации о просмотре фильма пользователем.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="movieId">Идентификатор фильма.</param>
        /// <param name="watchedTime">Время просмотра фильма (в минутах).</param>
        /// <returns>Ответ с результатом операции <see cref="ResponseDto"/>.</returns>
        Task<ResponseDto> AddUserViewedWatchedTimeAsync(Guid userId, Guid movieId, int watchedTime);

        /// <summary>
        /// Метод для получения списка просмотренных фильмов пользователя с пагинацией.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="currentPage">Текущая страница.</param>
        /// <param name="pageSize">Количество фильмов на странице.</param>
        /// <returns>Ответ с результатом операции <see cref="ResponseDto"/>.</returns>
        Task<ResponseDto> GetAllViewedMoviesOfUserAsync(Guid userId, int currentPage, int pageSize);
    }
}
