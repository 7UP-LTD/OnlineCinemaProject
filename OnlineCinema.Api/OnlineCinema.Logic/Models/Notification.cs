
namespace OnlineCinema.Logic.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string Url { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsRead { get; set; }
    }
}
