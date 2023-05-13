using System.ComponentModel.DataAnnotations;

namespace OnlineCinema.Logic.Dtos.AuthDtos
{
    /// <summary>
    /// DTO модель для сброса пароля пользователя.
    /// </summary>
    public class ResetPasswordDto
    {
        /// <summary>
        /// Токен доступа. Обязательное поле.
        /// </summary>
        [Required(ErrorMessage = "Должен быть указан токен доступа.")]
        public string Token { get; set; } = null!;

        /// <summary>
        /// Адрес электронной почты для сброса пароля. Обязательное поле.
        /// </summary>
        [Required(ErrorMessage = "Адрес электронной посты обязательное поле.")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Новый пароль. Обязательное поле.
        /// </summary>
        [Required(ErrorMessage = "Пароль обязательное поле.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 50 символов.")]
        public string NewPassword { get; set; } = null!;

        /// <summary>
        /// Подтверждение нового пароля. Обязательное поле.
        /// </summary>
        [Required(ErrorMessage = "Подтверждение пароля обязательное поле.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 50 символов.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
