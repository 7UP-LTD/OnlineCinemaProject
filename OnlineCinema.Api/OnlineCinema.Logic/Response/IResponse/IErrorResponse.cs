using OnlineCinema.Logic.Dtos;

namespace OnlineCinema.Logic.Response.IResponse
{
    /// <summary>
    /// Интерфейс, представляющий модель ошибки.
    /// </summary>
    public interface IErrorResponse
    {
        /// <summary>
        /// Возвращает модель ошибки "Внутренняя ошибка сервера".
        /// </summary>
        /// <returns>Модель ошибки "Внутренняя ошибка сервера".</returns>
        ErrorResponseDto InternalServerError();
    }
}
