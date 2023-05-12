using System;

namespace OnlineCinema.Data.Entities
{
    public class MovieGenreEntity : BaseEntity
    {
        public Guid GenreId { get; set; }
        public DicGenresEntity DicGenre { get; set; }

        public Guid MovieId { get; set; }
        public MovieEntity Movie { get; set; }
    }
}