using System;

namespace OnlineCinema.Data.Entities
{
    public class NotificationEntity : BaseEntity
    {
        public string  Subject  { get; set; }
        public string NotificationText { get; set; }
        public bool isRead { get; set; }
        public bool IsDeleted { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}