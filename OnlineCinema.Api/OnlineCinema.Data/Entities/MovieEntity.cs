using System;
using System.Collections.Generic;

namespace OnlineCinema.Data.Entities
{
    public class MovieEntity : BaseEntity
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

        public string? ContentUrl { get; set; }

        public List<MovieSeasonEntity> Seasons { get; set; } = new();
        
        public List<MovieCommentEntity> Comments { get; set; } = new();
        
        public List<MovieGenreEntity> Genres { get; set; } = new();
       
        public List<MovieTagEntity> Tags { get; set; } = new();
        
        public List<DicActorEntity> Actors { get; set; } = new();
        
        public List<DicDirectorEntity> Directors { get; set; } = new();
        
        public List<DicWriterEntity> Writers { get; set; } = new();
        
    }
}