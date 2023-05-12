
namespace OnlineCinema.Logic.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime DateRegistered { get; set; }

        public List<MovieComment> MovieComments { get; set; }

        public List<Movie> FavoriteMovies { get; set; }

        public List<Actor> FavoriteActors { get; set; }

        public List<Director> FaroriteDirectors { get; set; }

        public List<MovieMark> MoviesMarks { get; set; }

    }
}
