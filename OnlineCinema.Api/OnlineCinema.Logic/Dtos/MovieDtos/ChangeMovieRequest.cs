using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class ChangeMovieRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }
        
        /// <summary>
        /// год выпуска фильма
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        
        public IFormFile? filePoster { get; set; } 
        public IFormFile? fileBanner { get; set; }
        
        /// <summary>
        /// сериал или нет
        /// </summary>
        public bool IsSeries { get; set; }

        public Guid CountryId { get; set; }
        public string? ContentUrl { get; set; }
        
        public int AgeLimit { get; set; }

        public int Duration { get; set; }
        
        public List<Guid> Genres { get; set; } = new();
       
        public List<string> Tags { get; set; } = new();
        
    }
}