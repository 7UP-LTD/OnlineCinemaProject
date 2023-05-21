using System;
using System.Threading.Tasks;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.MovieDtos;

namespace OnlineCinema.Logic.Services.IServices
{
    /// <summary>
    /// Интерфейс для добавления фильма в список избранного пользователя.
    /// </summary>
    public interface IFavoriteMovieService
    {
        /// <summary>
        /// Получить список всех избранных фильмов пользователя.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        /// <returns>Ответ об успешности операции со списоком 
        /// избранных фильмов пользователя экземпляра см. <see cref="FavoriteMovieDto"/>.</returns>
        Task<ResponseDto> GetAllUserFavoriteMoviesAsync(Guid userId);

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
