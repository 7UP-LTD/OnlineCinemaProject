using System;
using System.Collections.Generic;

namespace OnlineCinema.Logic.Models
{
    public class MovieEpisodeDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }
        
        public string ContentUrl { get; set; }
        
        public Guid SeasonId { get; set; }
        public MovieSeasonDto MovieSeason { get; set; }
        
        public List<EpisodeCommentDto> Comments { get; set; } = new();
    }
}