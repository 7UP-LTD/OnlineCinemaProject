using System;

namespace OnlineCinema.Data.Entities
{
    public class MovieGenreEntity
    {
        public Guid GenreId { get; set; }

        public GenresDictionaryEntity Genre { get; set; }

        public Guid MovieId { get; set; }

        public MovieEntity Movie { get; set; }
    }
}