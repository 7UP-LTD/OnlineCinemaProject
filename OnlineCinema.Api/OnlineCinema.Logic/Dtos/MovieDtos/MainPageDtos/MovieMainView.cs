using System.Collections.Generic;

namespace OnlineCinema.Logic.Dtos.MovieDtos.MainPageDtos
{
    public class MovieMainView
    {
        public List<MovieView> NewMovies { get; set; }

        public List<MovieView> TopMovies { get; set; }
        
        public List<GenreMovies> GenreMovies { get; set; }
        
        public List<string> Banners { get; set; }
    }
}