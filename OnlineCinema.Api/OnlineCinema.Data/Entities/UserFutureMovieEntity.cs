using System;

namespace OnlineCinema.Data.Entities
{
    public class UserFutureMovieEntity : BaseEntity
    {
        public Guid MovieId { get; set; }
        public MovieEntity Movie { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}