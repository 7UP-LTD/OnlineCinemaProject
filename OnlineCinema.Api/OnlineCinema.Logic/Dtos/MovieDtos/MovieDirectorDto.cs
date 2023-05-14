using System;
using OnlineCinema.Logic.Dtos.DicDtos;
using OnlineCinema.Logic.Models;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class MovieDirectorDto
    {
        public Guid DirectorId { get; set; }
        public DicDirectorDto Director { get; set; }

        public Guid MovieId { get; set; }
        public MovieDto Movie { get; set; }
    }
}