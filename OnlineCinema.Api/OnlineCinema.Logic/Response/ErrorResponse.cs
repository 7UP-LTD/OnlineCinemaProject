using OnlineCinema.Logic.Dtos;
using OnlineCinema.Logic.Response.IResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Logic.Response
{
    /// <summary>
    /// Класс, представляющий модель ошибки.
    /// </summary>
    public class ErrorResponse : IErrorResponse
    {
        /// <inheritdoc/>
        public ErrorResponseDto InternalServerError() =>
            new()
            {
                ErrorMessage = "При выполнении операции произошла ошибка на сервере.",
                StatusCode = HttpStatusCode.InternalServerError,
            };
    }
}
