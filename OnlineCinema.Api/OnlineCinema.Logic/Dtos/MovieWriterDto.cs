using System;

namespace OnlineCinema.Logic.Models
{
    public class MovieWriterDto
    {
        public Guid WriterId { get; set; }
        public DicWriterDto Writer { get; set; }

        public Guid MovieId { get; set; }
        public MovieDto Movie { get; set; }
    }
}