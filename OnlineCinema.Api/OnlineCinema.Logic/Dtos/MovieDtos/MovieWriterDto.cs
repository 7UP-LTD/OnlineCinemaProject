using System;
using OnlineCinema.Logic.Dtos.DicDtos;
using OnlineCinema.Logic.Models;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class MovieWriterDto
    {
        public Guid WriterId { get; set; }
        public DicWriterDto Writer { get; set; }

        public Guid MovieId { get; set; }
        public MovieDto Movie { get; set; }
    }
}