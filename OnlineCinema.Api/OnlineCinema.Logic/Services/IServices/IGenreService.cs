using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.GenreDtos;

namespace OnlineCinema.Logic.Services.IServices
{
    /// <summary>
    /// Интерфейс сервиса для работы с жанрами.
    /// </summary>
    public interface IGenreService
    {
        /// <summary>
        /// Получает все жанры асинхронно.
        /// </summary>
        /// <returns>Коллекцию жанров.</returns>
        Task<ResponseDto> GetAllGenresAsync();

        /// <summary>
        /// Получает жанр по идентификатору асинхронно.
        /// </summary>
        /// <param name="genreId">Идентификатор жанра.</param>
        /// <returns>Жанр.</returns>
        Task<ResponseDto> GetGenreByIdAsync(Guid genreId);

        /// <summary>
        /// Создает жанр асинхронно.
        /// </summary>
        /// <param name="model">Модель создания жанра.</param>
        /// <returns>Результат операции создания жанра.</returns>
        Task<ResponseDto> CreateGenreAsync(GenreCreateDto model);

        /// <summary>
        /// Обновляет жанр асинхронно.
        /// </summary>
        /// <param name="model">Модель обновления жанра.</param>
        /// <returns>Результат операции обновления жанра.</returns>
        Task<ResponseDto> UpdateGenreAsync(GenreUpdateDto model);

        /// <summary>
        /// Обновляет жанр асинхронно.
        /// </summary>
        /// <param name="model">Модель обновления жанра.</param>
        /// <returns>Результат операции обновления жанра.</returns>
        Task<ResponseDto> DeleteGenreAsync(Guid genreId);

        /// <summary>
        /// Формирует ответ операции с кодом 400 Bad Request при невалидной модели.
        /// </summary>
        /// <param name="modelState">ModelState модели.</param>
        /// <returns>Объект ответа с кодом 400 Bad Request и ошибками валидации модели.</returns>
        ResponseDto ModelIsNotValid(ModelStateDictionary modelState);
    }
}
