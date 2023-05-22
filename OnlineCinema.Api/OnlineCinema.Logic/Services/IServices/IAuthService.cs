using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using OnlineCinema.Data.Entities;
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

        /// <summary>
        /// Проверяет, существует ли пользователь с указанным электронным адресом.
        /// </summary>
        /// <param name="email">Электронный адрес пользователя.</param>
        /// <returns>Значение true, если пользователь существует, в противном случае - false.</returns>
        Task<bool> IsUserExistFindByEmail(string email);

        /// <summary>
        /// Метод для выполнения входа пользователя через Google.
        /// </summary>
        /// <param name="email">Email пользователя.</param>
        /// <returns>Объект <see cref="UserManagerDto"/>, содержащий информацию о результате входа и сгенерированный токен доступа.</returns>
        Task<UserManagerDto> GoogleExternalLoginAsync(string email);

        /// <summary>
        /// Метод для регистрации пользователя через Google.
        /// </summary>
        /// <param name="claimsPrincipal">Утверждения пользователя полученные от провайдера аутентификации Google.</param>
        /// <returns>Объект <see cref="UserManagerDto"/>, содержащий информацию о результате регистрации и сгенерированный токен доступа.</returns>
        Task<UserManagerDto> GoogleExternalLoginRegisterAsync(ClaimsPrincipal claimsPrincipal);
    }
}
