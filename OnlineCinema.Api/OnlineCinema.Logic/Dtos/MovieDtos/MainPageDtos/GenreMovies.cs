using System.Collections.Generic;
using OnlineCinema.Logic.Dtos.GenreDtos;

namespace OnlineCinema.Logic.Dtos.MovieDtos.MainPageDtos
{
    public class GenreMovies
    {
        public GenreDto Genre { get; set; }
        
        public List<MovieView> Movies { get; set; }
    }
}