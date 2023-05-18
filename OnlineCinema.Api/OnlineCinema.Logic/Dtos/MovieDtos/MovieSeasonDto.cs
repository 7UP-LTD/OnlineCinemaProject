using System;
using System.Collections.Generic;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class MovieSeasonDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }
        
        /// <summary>
        /// дата выпуска сезона
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        
        public Guid MovieId { get; set; }
        
        public List<MovieEpisodeDto> Episodes { get; set; } = new();
    }
}