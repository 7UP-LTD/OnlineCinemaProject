using System;

namespace OnlineCinema.Logic.Models
{
    public class EpisodeCommentDto
    {
        public string Text { get; set; }
        
        public Guid EpisodeId { get; set; }
        public MovieEpisodeDto Episode { get; set; }
    }
}