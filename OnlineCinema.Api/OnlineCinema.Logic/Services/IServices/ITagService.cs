using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Dtos.TagDtos;

namespace OnlineCinema.Logic.Services.IServices
{
    /// <summary>
    /// Интерфейс сервиса для работы с тегами.
    /// </summary>
    public interface ITagService
    {
        /// <summary>
        /// Получает все теги.
        /// </summary>
        /// <returns>Объект ответа с кодом 200 OK и списком тегов.</returns>
        Task<ResponseDto> GetAllTagsAsync();

        /// <summary>
        /// Получает тег по идентификатору.
        /// </summary>
        /// <param name="tagId">Идентификатор тега.</param>
        /// <returns>Объект ответа с кодом 200 OK и тегом.</returns>
        Task<ResponseDto> GetTagByIdAsync(Guid tagId);

        /// <summary>
        /// Получает тег по намиенованию.
        /// </summary>
        /// <param name="tagName">Наименование тега.</param>
        /// <returns>Объект ответа с кодом 200 OK и тегом.</returns>
        Task<ResponseDto> GetTagByName(string tagName);
        
        /// <summary>
        /// Создает новый тег.
        /// </summary>
        /// <param name="model">Данные нового тега.</param>
        /// <returns>Объект ответа с кодом 200 OK при успешном создании.</returns>
        Task<ResponseDto> CreateTagAsync(TagCreateDto model);

        /// <summary>
        /// Обновляет существующий тег.
        /// </summary>
        /// <param name="model">Данные обновляемого тега.</param>
        /// <returns>Объект ответа с кодом 200 OK при успешном обновлении.</returns>
        Task<ResponseDto> UpdateTagAsync(TagDto model);

        /// <summary>
        /// Удаляет тег по идентификатору.
        /// </summary>
        /// <param name="tagId">Идентификатор тега.</param>
        /// <returns>Объект ответа с кодом 200 OK при успешном удалении.</returns>
        Task<ResponseDto> DeleteTagAsync(Guid tagId);

        /// <summary>
        /// Формирует ответ операции с кодом 400 Bad Request при валидной модели.
        /// </summary>
        /// <param name="modelState">ModelState модели.</param>
        /// <returns>Объект ответа с кодом 400 Bad Request и ошибками валидации модели.</returns>
        ResponseDto ModelStateIsValid(ModelStateDictionary modelState);
    }
}
