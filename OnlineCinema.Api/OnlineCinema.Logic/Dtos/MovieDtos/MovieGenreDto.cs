using System;
using OnlineCinema.Logic.Dtos.DicDtos;
using OnlineCinema.Logic.Models;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class MovieGenreDto
    {
        public Guid GenreId { get; set; }
       
        public Guid MovieId { get; set; }
       
    }
}