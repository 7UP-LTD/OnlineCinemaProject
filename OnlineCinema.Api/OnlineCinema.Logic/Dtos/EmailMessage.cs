namespace OnlineCinema.Logic.Dtos
{
    /// <summary>
    /// Класс, представляющий сообщение для подтверждения email.
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// Заголовок сообщения.
        /// </summary>
        public string Subject { get; set; } = null!;

        /// <summary>
        /// HTML-сообщение для отправки.
        /// </summary>
        public string HtmlMessage { get; set; } = null!;
    }
}
