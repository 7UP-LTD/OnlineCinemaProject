using System.Net;

namespace OnlineCinema.Logic.Dtos
{
    /// <summary>
    /// Ответ в случае ошибки.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Сообщение.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Код ошибки.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }
}
