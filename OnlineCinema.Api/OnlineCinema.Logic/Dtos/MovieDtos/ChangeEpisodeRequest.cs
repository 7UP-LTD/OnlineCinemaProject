using System;
using System.Collections.Generic;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class ChangeEpisodeRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }
        
        public string ContentUrl { get; set; }
        
        public Guid SeasonId { get; set; }
   
    }
}