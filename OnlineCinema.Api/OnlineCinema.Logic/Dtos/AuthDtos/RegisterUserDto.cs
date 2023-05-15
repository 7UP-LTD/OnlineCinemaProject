
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineCinema.Logic.Dtos.AuthDtos
{
    /// <summary>
    /// DTO модель для регистрации пользователя.
    /// </summary>
    public class RegisterUserDto
    {
        /// <summary>
        /// Имя пользователя. Обязательное поле.
        /// </summary>
        [Required(ErrorMessage = "Имя обязательное поле.")]
        [DisplayName("Имя")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Фамилия пользователя. Обязательное поле.
        /// </summary>
        [Required(ErrorMessage = "Фамилия обязательное поле.")]
        public string SurName { get; set; } = null!;

        /// <summary>
        /// Имя пользователя. Минимальная длина 4 максимальная 50.
        /// </summary>
        [Required(ErrorMessage = "Имя пользователя обязательное поле.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Длина имени пользователя должна быть не меньше 4 и не больше 50 символов.")]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Фото пользователя. Не обязательное поле.
        /// </summary>
        [DisplayName("Иконка пользователя")]
        public string? Icon { get; set; }

        /// <summary>
        /// Электронная почта пользователя. Обязательное поле.
        /// </summary>
        [Required(ErrorMessage = "Электронная почта обязательное поле.")]
        [EmailAddress(ErrorMessage = "Укажите почту. Пример: user@example.com")]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Пароль пользователя. Обязательное поле. Минимальная длина 6 символов.
        /// </summary>
        [Required(ErrorMessage = "Пароль обязательное поле.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть не меньше 6 и не больше 50 символов.")]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Подтверждения пароля. Обязательно поле. Минимальная длина 6 символов.
        /// </summary>
        [Required(ErrorMessage = "Подтверждение пароля обязательное поле.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть не меньше 6 и не больше 50 символов.")]
        public string ConfirmPassword { get; set; } = null!;

        /// <summary>
        /// URL для переадресации после успешной регистрации. Обязательное поле
        /// </summary>
        [Required(ErrorMessage = "Не указан URL для переадресации после успешной регистрации пользователя.")]
        public string ConfirmRedirectUrl { get; set; } = null!;
    }
}
