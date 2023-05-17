using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Response.IResponse;
using System.Net;

namespace OnlineCinema.Logic.Response
{
    /// <summary>
    /// Реализация интерфейса для формирования ответа операции.
    /// </summary>
    public class OperationResponse : IOperationResponse
    {
        /// <inheritdoc/>
        public ResponseDto SuccessResponse(object? result = null) =>
            new()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = result
            };

        /// <inheritdoc/>
        public ResponseDto NotFound(List<string> errors, object? result = null) =>
             new()
             {
                 StatusCode = HttpStatusCode.NotFound,
                 IsSuccess = false,
                 Result = result,
                 Errors = errors
             };

        /// <inheritdoc/>
        public ResponseDto BadRequest(List<string> errors, object? result = null) =>
            new()
            {
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccess = false,
                Result = result,
                Errors = errors
            };

        /// <inheritdoc/>
        public ResponseDto CreatedSuccessfully(object? result = null) =>
            new()
            {
                StatusCode = HttpStatusCode.Created,
                Result = result is null ? "Информация успешно добавлена." : result,
                IsSuccess = true,
            };

        /// <inheritdoc/>
        public ResponseDto UpdatedSuccessfully() =>
            new()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = "Информация успешно обновлена."
            };

        /// <inheritdoc/>
        public ResponseDto DeleteSuccessfully() =>
            new()
            {
                StatusCode = HttpStatusCode.NoContent,
                IsSuccess = true,
                Result = "Информация успешно удалена."
            };

        /// <inheritdoc/>
        public ResponseDto ModelStateIsNotValid(ModelStateDictionary modelState) =>
            new()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Result = "Одно или несколько полей не валидны.",
                IsSuccess = false,
                Errors = modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
            };
    }
}
