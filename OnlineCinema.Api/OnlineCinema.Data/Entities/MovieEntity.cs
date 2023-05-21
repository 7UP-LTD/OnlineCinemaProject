using System;
using System.Collections.Generic;

namespace OnlineCinema.Data.Entities
{
    public class MovieEntity : BaseEntity
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
        
        public string MovieBannerUrl { get; set; }

        //public Guid BannerFileId { get; set; }

        /// <summary>
        /// сериал или нет
        /// </summary>
        public bool IsSeries { get; set; }

        public string? ContentUrl { get; set; }

        public Guid CountryId { get; set; }

        public DicCountryEntity Country { get; set; }

        public int AgeLimit { get; set; }

        public int Duration { get; set; }
        public List<MovieSeasonEntity> Seasons { get; set; } = new();

        public List<MovieCommentEntity> Comments { get; set; } = new();

        public List<MovieGenreEntity> Genres { get; set; } = new();

        public List<MovieTagEntity> Tags { get; set; } = new();

        public List<MovieActorEntity> Actors { get; set; } = new();

        public List<MovieDirectorEntity> Directors { get; set; } = new();

        public List<MovieWriterEntity> Writers { get; set; } = new();
        
        public List<ImageEntity> Images { get; set; } = new();
    }
}