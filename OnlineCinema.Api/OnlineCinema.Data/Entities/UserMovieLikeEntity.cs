using System;

namespace OnlineCinema.Data.Entities
{
    public class UserMovieLikeEntity : BaseEntity
    {
        public Guid MovieId { get; set; }
        public MovieEntity Movie { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        
        public bool IsLike { get; set; }
    }
}