﻿using System;
using System.Collections.Generic;

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

        /// <summary>
        /// ссылка на картинку-заставку
        /// </summary>
        public string MoviePosterUrl { get; set; }

        /// <summary>
        /// сериал или нет
        /// </summary>
        public bool IsSeries { get; set; }

        public Guid CountryId { get; set; }
        public string? ContentUrl { get; set; }
        
        public int AgeLimit { get; set; }

        public int Duration { get; set; }
        
        public List<Guid> Genres { get; set; } = new();
       
        public List<Guid> Tags { get; set; } = new();
        
    }
}