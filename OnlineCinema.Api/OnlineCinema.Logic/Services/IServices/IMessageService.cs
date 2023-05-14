using System.Threading.Tasks;
using OnlineCinema.Logic.Helpers;

namespace OnlineCinema.Logic.Services.IServices
{
    public interface IMessageService
    {
        Task<EmailMessage> GetConfirmationEmailHtmlAsync(string confirmationLink);

        Task<EmailMessage> GetResetEmailHtmlAsync(string resetLink);
    }
}
