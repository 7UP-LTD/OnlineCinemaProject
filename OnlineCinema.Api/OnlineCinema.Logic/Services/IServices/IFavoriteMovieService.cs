using OnlineCinema.Logic.Dtos;

namespace OnlineCinema.Logic.Services.IServices
{
    /// <summary>
    /// Интерфейс для добавления фильма в список избранного пользователя.
    /// </summary>
    public interface IFavoriteMovieService
    {
        /// <summary>
        /// Получает все любимые фильмы пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="currentPage">Текущая страница.</param>
        /// <param name="moviesPerPage">Количество фильмов на странице.</param>
        /// <returns>Задача, возвращающая объект <see cref="ResponseDto"/>.</returns>
        Task<ResponseDto> GetAllUserFavoriteMoviesAsync(Guid userId, int currentPage, int moviesPerPage);

        /// <summary>
        /// Добавить фильм в избранное пользователя.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        /// <param name="movieId">ID фильма который надо добавить.</param>
        /// <returns>Ответ об успешности операции см. <see cref="ResponseDto"/>.</returns>
        Task<ResponseDto> AddToFavoritesAsync(Guid userId, Guid movieId);

        /// <summary>
        /// Удаление фильма из списка избранного пользователя.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        /// <param name="movieId">ID фильма который надо добавить.</param>
        /// <returns>Ответ об успешности операции см. <see cref="ResponseDto"/>.</returns>
        Task<ResponseDto> DeleteFromFavoritesAsync(Guid userId, Guid movieId);
    }
}
