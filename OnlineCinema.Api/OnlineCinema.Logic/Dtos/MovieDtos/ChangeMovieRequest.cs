using System;
using System.Collections.Generic;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class ChangeMovieRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        /// <summary>
        /// дата добавления на сайт
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// год выпуска фильма
        /// </summary>
        public DateTime ReleaseYear { get; set; }

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
        
        public List<Guid> Actors { get; set; } = new();
        
        public List<Guid> Directors { get; set; } = new();
        
        public List<Guid> Writers { get; set; } = new();
    }
}