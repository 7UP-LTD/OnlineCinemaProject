
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

        public List<Actor> Actors { get; set; }

        public List<Director> Directors { get; set; }

        public List<Genre> Genres { get; set; }

        public List<Country> Countries { get; set; }
    }
}
