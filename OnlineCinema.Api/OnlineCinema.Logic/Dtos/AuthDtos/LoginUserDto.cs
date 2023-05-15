using System.ComponentModel.DataAnnotations;

namespace OnlineCinema.Logic.Dtos.AuthDtos
{
    /// <summary>
    /// DTO модель для входа в приложение.
    /// </summary>
    public class LoginUserDto
    {
        /// <summary>
        /// Имя пользователя. Обязательное поле
        /// </summary>
        [Required(ErrorMessage = "Имя пользователя обязательное поле.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Длина пароля должна быть ")]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Пароль обязательное поле. Минимальная длина пароля 6 символов.
        /// </summary>
        [Required(ErrorMessage = "Пароля обязательное поле.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть не меньше 6 и не больше 50 символов.")]
        public string Password { get; set; } = null!;
    }
}
