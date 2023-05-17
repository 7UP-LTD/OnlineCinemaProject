using System.Collections.Generic;
using OnlineCinema.Logic.Dtos.AuthDtos;
using OnlineCinema.Logic.Response.IResponse;

namespace OnlineCinema.Logic.Response
{
    /// <summary>
    /// Класс, представляющий ответ операции управления пользователями.
    /// </summary>
    public class UserManagerResponse : IUserManagerResponse
    {
        /// <inheritdoc/>
        public UserManagerDto ConfirmEmailFailed(List<string> errors) =>
            new()
            {
                Message = "Почта не подтверждена.",
                IsSuccess = false,
                Errors = errors
            };

        /// <inheritdoc/>
        public UserManagerDto ConfirmEmailSuccessfully() =>
            new()
            {
                Message = "Электронная почта подтверждена.",
                IsSuccess = true
            };

        /// <inheritdoc/>
        public UserManagerDto DublicateEmail(string email) =>
            new()
            {
                Message = "Пользователь не зарегистрирован.",
                Errors = new List<string> { $"Пользователь с электронной почтой {email} уже существует." }
            };

        /// <inheritdoc/>
        public UserManagerDto EmailForResetPasswordSentSuccessfully() =>
            new()
            {
                Message = "Письмо для сброса пароля было успешно отправлено.",
                IsSuccess = true
            };

        /// <inheritdoc/>
        public UserManagerDto EntryDenied(List<string> errors) =>
            new()
            {
                Message = "Вход запрещён",
                IsSuccess = false,
                Errors = errors
            };

        /// <inheritdoc/>
        public UserManagerDto InvalidConfirmPassword() =>
            new()
            {
                Message = "Пароли не совпадают.",
                IsSuccess = false,
                Errors = new List<string> { "Подтверждения пароля не совпадает с паролем." }
            };

        /// <inheritdoc/>
        public UserManagerDto NotFound(List<string>? errors) =>
            new()
            {
                Message = "Пользователь не найден",
                IsSuccess = false,
                Errors = errors
            };

        /// <inheritdoc/>
        public UserManagerDto InvalidToken(List<string>? errors) =>
            new()
            {
                Message = "Проблемы с токеном.",
                IsSuccess = false,
                Errors = errors
            };

        /// <inheritdoc/>
        public UserManagerDto ResetPasswordFailed(List<string> errors) =>
            new()
            {
                Message = "Неудалось сбросить пароль",
                IsSuccess = false,
                Errors = errors
            };

        /// <inheritdoc/>
        public UserManagerDto ResetPasswordSuccessfully() =>
            new()
            {
                Message = "Пароль был успешно сброшен.",
                IsSuccess = true
            };

        /// <inheritdoc/>
        public UserManagerDto UserNameAlreadyExist(string userName) =>
            new()
            {
                Message = "Имя пользователь уже существует.",
                IsSuccess = false,
                Errors = new List<string> { $"Пользователь с таким именем пользователя уже существует {userName}." }
            };

        /// <inheritdoc/>
        public UserManagerDto UserRegisterFailed(List<string> errors) =>
            new()
            {
                Message = "Пользователь не зарегистрирован.",
                IsSuccess = false,
                Errors = errors
            };

        /// <inheritdoc/>
        public UserManagerDto UserRegisterSuccessfully() =>
            new()
            {
                Message = "Пользователь успешно зарегистирован.",
                IsSuccess = true
            };

        /// <inheritdoc/>
        public UserManagerDto EntrySuccessfully(string token) =>
            new()
            {
                Message = token,
                IsSuccess = true
            };
    }
}
