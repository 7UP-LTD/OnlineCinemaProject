using OnlineCinema.Logic.Dtos.AuthDtos;

namespace OnlineCinema.Logic.ServiceResponses.IServiceResponses
{
    /// <summary>
    /// Интерфейс для формирования ответов операций пользователя.
    /// </summary>
    public interface IUserManagerResponse
    {
        /// <summary>
        /// Формирует ответ с сообщением "Пользователь не найден".
        /// </summary>
        /// <param name="errors">Список ошибок.</param>
        /// <returns>Ответ операции с сообщением "Пользователь не найден".</returns>
        UserManagerDto NotFound(List<string>? errors);

        /// <summary>
        /// Формирует ответ с сообщением "Неверный токен".
        /// </summary>
        /// <param name="errors">Список ошибок.</param>
        /// <returns>Ответ операции с сообщением "Неверный токен".</returns>
        UserManagerDto InvalidToken(List<string>? errors);

        /// <summary>
        /// Формирует ответ с сообщением "Подтверждение электронной почты выполнено успешно".
        /// </summary>
        /// <returns>Ответ операции с сообщением "Подтверждение электронной почты выполнено успешно".</returns>
        UserManagerDto ConfirmEmailSuccessfully();

        /// <summary>
        /// Формирует ответ с сообщением "Подтверждение электронной почты не удалось".
        /// </summary>
        /// <param name="errors">Список ошибок.</param>
        /// <returns>Ответ операции с сообщением "Подтверждение электронной почты не удалось".</returns>
        UserManagerDto ConfirmEmailFailed(List<string> errors);

        /// <summary>
        /// Формирует ответ с сообщением "Электронное письмо для сброса пароля успешно отправлено".
        /// </summary>
        /// <returns>Ответ операции с сообщением "Электронное письмо для сброса пароля успешно отправлено".</returns>
        UserManagerDto EmailForResetPasswordSentSuccessfully();

        /// <summary>
        /// Формирует ответ с сообщением "Доступ запрещен".
        /// </summary>
        /// <param name="errors">Список ошибок.</param>
        /// <returns>Ответ операции с сообщением "Доступ запрещен".</returns>
        UserManagerDto EntryDenied(List<string> errors);

        /// <summary>
        /// Формирует ответ с сообщением "Вход выполнен успешно" и токеном пользователя.
        /// </summary>
        /// <param name="token">Токен пользователя.</param>
        /// <returns>Ответ операции с сообщением "Вход выполнен успешно" и токеном пользователя.</returns>
        UserManagerDto EntrySuccessfully(string token);

        /// <summary>
        /// Формирует ответ с сообщением "Неверное подтверждение пароля".
        /// </summary>
        /// <returns>Ответ операции с сообщением "Неверное подтверждение пароля".</returns>
        UserManagerDto InvalidConfirmPassword();

        /// <summary>
        /// Формирует ответ с сообщением "Пользователь с таким именем пользователя уже существует".
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Ответ операции с сообщением "Пользователь с таким именем пользователя уже существует".</returns>
        UserManagerDto UserNameAlreadyExist(string userName);

        /// <summary>
        /// Формирует ответ с сообщением "Пользователь успешно зарегистрирован".
        /// </summary>
        /// <returns>Ответ операции с сообщением "Пользователь успешно зарегистрирован".</returns>
        UserManagerDto UserRegisterSuccessfully();

        /// <summary>
        /// Формирует ответ с сообщением "Ошибка при регистрации пользователя".
        /// </summary>
        /// <param name="errors">Список ошибок.</param>
        /// <returns>Ответ операции с сообщением "Ошибка при регистрации пользователя".</returns>
        UserManagerDto UserRegisterFailed(List<string> errors);

        /// <summary>
        /// Формирует ответ с сообщением "Дубликат электронной почты".
        /// </summary>
        /// <param name="email">Адрес электронной почты.</param>
        /// <returns>Ответ операции с сообщением "Дубликат электронной почты".</returns>
        UserManagerDto DublicateEmail(string email);

        /// <summary>
        /// Формирует ответ с сообщением "Пароль успешно сброшен".
        /// </summary>
        /// <returns>Ответ операции с сообщением "Пароль успешно сброшен".</returns>
        UserManagerDto ResetPasswordSuccessfully();

        /// <summary>
        /// Формирует ответ с сообщением "Ошибка при сбросе пароля".
        /// </summary>
        /// <param name="errors">Список ошибок.</param>
        /// <returns>Ответ операции с сообщением "Ошибка при сбросе пароля".</returns>
        UserManagerDto ResetPasswordFailed(List<string> errors);
    }
}
