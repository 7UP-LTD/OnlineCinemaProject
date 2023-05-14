using System;

namespace OnlineCinema.Logic.Models
{
    public class MovieDirectorDto
    {
        public Guid DirectorId { get; set; }
        public DicDirectorDto Director { get; set; }

        public Guid MovieId { get; set; }
        public MovieDto Movie { get; set; }
    }
}