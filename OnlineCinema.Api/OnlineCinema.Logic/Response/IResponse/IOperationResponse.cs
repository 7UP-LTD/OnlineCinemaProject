using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineCinema.Logic.Dtos;

namespace OnlineCinema.Logic.Response.IResponse
{
    /// <summary>
    /// Интерфейс для формирования ответа операции.
    /// </summary>
    public interface IOperationResponse
    {
        /// <summary>
        /// Формирует успешный ответ операции.
        /// </summary>
        /// <param name="result">Дополнительные данные результата.</param>
        /// <returns>Объект успешного ответа.</returns>
        ResponseDto SuccessResponse(object? result = null);

        /// <summary>
        /// Формирует ответ операции с кодом 404 Not Found.
        /// </summary>
        /// <param name="errors">Список ошибок.</param>
        /// <param name="result">Дополнительные данные результата.</param>
        /// <returns>Объект ответа с кодом 404 Not Found.</returns>
        ResponseDto NotFound(List<string> errors, object? result = null);

        /// <summary>
        /// Формирует ответ операции с кодом 400 Bad Request.
        /// </summary>
        /// <param name="errors">Список ошибок.</param>
        /// <param name="result">Дополнительные данные результата.</param>
        /// <returns>Объект ответа с кодом 400 Bad Request.</returns>
        ResponseDto BadRequest(List<string> errors, object? result = null);

        /// <summary>
        /// Формирует успешный ответ операции с кодом 201 Created.
        /// </summary>
        /// <returns>Объект успешного ответа с кодом 201 Created.</returns>
        ResponseDto CreatedSuccessfully();

        /// <summary>
        /// Формирует успешный ответ операции с кодом 200 OK обновления данных.
        /// </summary>
        /// <returns>Объект успешного ответа с кодом 200 OK обновления данных.</returns>
        ResponseDto UpdatedSuccessfully();

        /// <summary>
        /// Формирует успешный ответ операции с кодом 204 NoContetудаления данных.
        /// </summary>
        /// <returns>Объект успешного ответа с кодом 204 NoContet удаления данных.</returns>
        ResponseDto DeleteSuccessfully();

        /// <summary>
        /// Формирует ответ операции с кодом 400 Bad Request при невалидной модели.
        /// </summary>
        /// <param name="modelState">ModelState модели.</param>
        /// <returns>Объект ответа с кодом 400 Bad Request и ошибками валидации модели.</returns>
        ResponseDto ModelStateIsNotValid(ModelStateDictionary modelState);
    }
}
