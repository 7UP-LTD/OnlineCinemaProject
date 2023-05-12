using System;

namespace OnlineCinema.Data.Entities
{
    public class NotificationEntity : BaseEntity
    {
        public string NotificationText { get; set; }

        public Guid UserId { get; set; }

        public UserEntity User { get; set; }
    }
}