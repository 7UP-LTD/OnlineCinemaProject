using System.Collections.Generic;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class MoviesListViewModel
    {
        public List<MovieDto> Movies { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}