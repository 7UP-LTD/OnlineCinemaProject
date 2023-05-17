using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MimeKit;

namespace OnlineCinema.Logic.Services
{
    public partial class EmailSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailSettings = _configuration.GetSection("Google").Get<MailSettings>()!;
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.From));
            emailToSend.To.Add(new MailboxAddress(email, email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            using var emailClient = new SmtpClient();
            await emailClient.ConnectAsync(mailSettings.Host, mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await emailClient.AuthenticateAsync(mailSettings.UserName, mailSettings.Password);
            await emailClient.SendAsync(emailToSend);
            await emailClient.DisconnectAsync(true);
        }
    }
}
