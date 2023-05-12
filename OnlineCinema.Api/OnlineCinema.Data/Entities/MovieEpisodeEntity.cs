using System;
using System.Collections.Generic;

namespace OnlineCinema.Data.Entities
{
    public class MovieEpisodeEntity : BaseEntity
    {
        public string Name { get; set; }

        public string? Description { get; set; }
        
        public string ContentUrl { get; set; }
        
        public Guid SeasonId { get; set; }
        public MovieSeasonEntity MovieSeason { get; set; }
        
        public List<EpisodeCommentEntity> Comments { get; set; } = new();
    
    }
}