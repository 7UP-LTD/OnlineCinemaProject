using Microsoft.AspNetCore.Authentication;
using OnlineCinema.Logic.Dtos.AuthDtos;

namespace OnlineCinema.Logic.Services.IServices
{
    /// <summary>
    /// Интерфейс сервиса аутентификации и авторизации.
    /// </summary>
    public interface IAuthService 
    {
        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        /// <param name="model">DTO для регистрации пользователя.</param>
        /// <returns>Ответ менеджера пользователя.</returns>
        Task<UserManagerDto> RegisterUserAsync(RegisterUserDto model);

        /// <summary>
        /// Аутентификация пользователя.
        /// </summary>
        /// <param name="model">DTO для аутентификации пользователя.</param>
        /// <returns>Ответ менеджера пользователя.</returns>
        Task<UserManagerDto> LoginUserAsync(LoginUserDto model);

        /// <summary>
        /// Подтверждение электроной почты.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        /// <param name="token">Токен доступа.</param>
        /// <returns>Ответ менеджера пользователя.</returns>
        Task<UserManagerDto> ConfirmEmailAsync(string userId, string token);

        /// <summary>
        /// Сменить пароль пользователя.
        /// </summary>
        /// <param name="email">Электронная почта для подтверждения смены пароля.</param>
        /// <param name="redirectUrl">URL для перенаправления для сброса пароля.</param>
        /// <returns>Ответ менеджера пользователя.</returns>
        Task<UserManagerDto> ForgetPasswordAsync(string email, string redirectUrl);

        /// <summary>
        /// Сброс пароля пользователя.
        /// </summary>
        /// <param name="model">DTO для сброса пароля.</param>
        /// <returns>Ответ менеджера пользователя.</returns>
        Task<UserManagerDto> ResetPasswordAsync(ResetPasswordDto model);
    }
}
