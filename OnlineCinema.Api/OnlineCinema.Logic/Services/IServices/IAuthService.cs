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
        Task<UserManagerResponse> RegisterUserAsync(RegisterUserDto model);

        /// <summary>
        /// Аутентификация пользователя.
        /// </summary>
        /// <param name="model">DTO для аутентификации пользователя.</param>
        /// <returns>Ответ менеджера пользователя.</returns>
        Task<UserManagerResponse> LoginUserAsync(LoginUserDto model);
    }
}
