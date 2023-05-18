using System;
using System.Collections.Generic;
using OnlineCinema.Logic.Models;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class MovieEpisodeDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }
        
        public string ContentUrl { get; set; }
        
        public Guid SeasonId { get; set; }
        
       // public List<EpisodeCommentDto> Comments { get; set; } = new();
    }
}