using System;

namespace OnlineCinema.Logic.Models
{
    public class MovieGenreDto
    {
        public Guid GenreId { get; set; }
        public DicGenreDto DicGenre { get; set; }

        public Guid MovieId { get; set; }
        public MovieDto Movie { get; set; }
    }
}