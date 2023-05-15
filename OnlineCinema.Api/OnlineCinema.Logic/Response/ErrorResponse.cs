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
    internal class ErrorResponse : IErrorResponse
    {
        public ErrorResponseDto InternalServerError() =>
            new()
            {
                ErrorMessage = "При выполнении операции произошла ошибка на сервере.",
                StatusCode = HttpStatusCode.InternalServerError,
            };
    }
}
