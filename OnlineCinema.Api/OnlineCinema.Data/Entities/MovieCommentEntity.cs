using System;

namespace OnlineCinema.Data.Entities
{
    public class MovieCommentEntity : BaseEntity
    {
        public string CommentText { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        
        public Guid MovieId { get; set; }
        public MovieEntity Movie { get; set; }
    }
}