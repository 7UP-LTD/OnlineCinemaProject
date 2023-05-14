using System;

namespace OnlineCinema.Logic.Models
{
    public class MovieTagDto
    {
        public Guid TagId { get; set; }
        public DicTagDto Tag { get; set; }

        public Guid MovieId { get; set; }
        public MovieDto Movie { get; set; }
    }
}