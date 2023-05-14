using System;
using Microsoft.AspNetCore.Identity;

namespace OnlineCinema.Data.Entities
{
    /// <summary>
    /// Представляет сущность пользователя в приложении. 
    /// Наследование от IdentityUser с переопределением ID на Guid.
    /// </summary>
    public class UserEntity : IdentityUser<Guid>
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Фамилию пользователя.
        /// </summary>
        public string? Surname { get; set; }

        /// <summary>
        /// Иконка пользователя.
        /// </summary>
        public string? Icon { get; set; }
    }
}