using System;
using OnlineCinema.Logic.Dtos.DicDtos;
using OnlineCinema.Logic.Models;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class MovieTagDto
    {
        public Guid TagId { get; set; }
        public DicTagDto Tag { get; set; }

        public Guid MovieId { get; set; }
        public MovieDto Movie { get; set; }
    }
}