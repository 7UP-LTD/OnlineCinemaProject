using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class FavoriteMovieDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string MoviePosterUrl { get; set; }
    }
}
