
namespace OnlineCinema.Logic.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public double Rating { get; set; }

        public bool IsViewed { get; set; }

        public string PosterUrl { get; set; }

        public List<Actor> Actors { get; set; }

        public List<Director> Directors { get; set; }

        public List<Country> Countries { get; set; }

        public ICollection<MovieComment> MovieComments { get; set; }

        public ICollection<MovieMark> MovieMarks { get; set; }

        public ICollection<Genre> MovieGenres { get; set; }

        public ICollection<MovieTag> MovieTags { get; set; }
    }
}
