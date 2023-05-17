using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using OnlineCinema.Logic.Helpers;
using OnlineCinema.Logic.Services.IServices;

namespace OnlineCinema.Logic.Services
{
    /// <summary>
    /// Класс-помощник для создания объектов типа <see cref="EmailMessage"/>.
    /// </summary>
    public class MessageService : IMessageService
    {
        private readonly EmailMessage _message;
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MessageService"/>.
        /// </summary>
        /// <param name="env">Среда выполнения, предоставляющая информацию о приложении и хостинге.</param>
        public MessageService(IWebHostEnvironment env)
        {
            _env = env;
            _message = new();
        }

        /// <summary>
        /// Получает HTML-письмо с ссылкой для подтверждения электронной почты.
        /// </summary>
        /// <param name="confirmationLink">Ссылка для подтверждения электронной почты.</param>
        /// <returns>Объект типа <see cref="EmailMessage"/>, представляющий HTML-письмо с ссылкой для подтверждения электронной почты.</returns>
        public async Task<EmailMessage> GetConfirmationEmailHtmlAsync(string confirmationLink)
        {
            var filePath = Path.Combine(_env.ContentRootPath, "EmailTemplates", "email-confirmation.html");
            var htmlTemplate = await File.ReadAllTextAsync(filePath);
            _message.HtmlMessage = htmlTemplate.Replace("[CONFIRMATION_LINK]", confirmationLink);
            _message.Subject = "Подтверждение электронной почты.";
            return _message;
        }

        /// <summary>
        /// Получает HTML-письмо с ссылкой для сброса пароля.
        /// </summary>
        /// <param name="resetLink">Ссылка для сброса пароля.</param>
        /// <returns>Объект типа <see cref="EmailMessage"/>, представляющий HTML-письмо с ссылкой для сброса пароля.</returns>
        public async Task<EmailMessage> GetResetEmailHtmlAsync(string resetLink)
        {
            var filePath = Path.Combine(_env.ContentRootPath, "EmailTemplates", "email-reset-password.html");
            var htmlTemplate = await File.ReadAllTextAsync(filePath);
            _message.HtmlMessage = htmlTemplate.Replace("[RESET_LINK]", resetLink);
            _message.Subject = "Сброс пароля.";
            return _message;
        }
    }
}
