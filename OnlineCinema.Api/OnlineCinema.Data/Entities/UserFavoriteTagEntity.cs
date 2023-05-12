using System;

namespace OnlineCinema.Data.Entities
{
    public class UserFavoriteTagEntity : BaseEntity
    {
        public Guid TagId { get; set; }

        public TagsDictionaryEntity Tag { get; set; }

        public Guid UserId { get; set; }

        public UserEntity User { get; set; }
    }
}