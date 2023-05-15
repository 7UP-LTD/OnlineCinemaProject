using System.Net;

namespace OnlineCinema.Logic.Dtos
{
    /// <summary>
    /// Класс, представляющий объект ответа.
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// Код состояния HTTP.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Флаг указывающий на успешность операции.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Список ошибок, если есть.
        /// </summary>
        public List<string>? Errors { get; set; }

        /// <summary>
        /// Результат операции.
        /// </summary>
        public object? Result { get; set; }
    }
}
