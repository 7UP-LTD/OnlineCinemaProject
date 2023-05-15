using System.Collections.Generic;

namespace OnlineCinema.Logic.Dtos.AuthDtos
{
    /// <summary>
    /// Ответ менеджера пользователей.
    /// </summary>
    public class UserManagerDto
    {
        /// <summary>
        /// Сообщение об операции.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Успешность операции. По умолчанию false.
        /// </summary>
        public bool IsSuccess { get; set; } = false;

        /// <summary>
        /// Список ошибок.
        /// </summary>
        public IEnumerable<string>? Errors { get; set; }
    }
}
