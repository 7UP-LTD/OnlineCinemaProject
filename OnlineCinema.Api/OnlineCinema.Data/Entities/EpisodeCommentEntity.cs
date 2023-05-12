using System;

namespace OnlineCinema.Data.Entities
{
    public class EpisodeCommentEntity : BaseEntity
    {
        public string CommentText { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        
        public Guid EpisodeId { get; set; }
        public MovieEpisodeEntity Episode { get; set; }
    }
}