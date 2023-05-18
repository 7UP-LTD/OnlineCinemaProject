using System;

namespace OnlineCinema.Data.Entities
{
    public class MovieGenreEntity : BaseEntity
    {
        public Guid DicGenreId { get; set; }
        public DicGenreEntity DicGenre { get; set; }

        public Guid MovieId { get; set; }
        public MovieEntity Movie { get; set; }
    }
}