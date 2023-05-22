using System;

namespace OnlineCinema.Logic.Dtos.MovieDtos.MainPageDtos
{
    public class MovieView
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string MoviePosterUrl { get; set; }

        public string MovieBannerUrl { get; set; }
    }
}