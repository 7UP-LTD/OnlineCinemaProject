
namespace OnlineCinema.Logic.Models
{
    public class UserFavoriteMovie
    {
        public int UserFavoriteMovieId { get; set; }

        public int MovieId { get; set; }

        public int UserId { get; set; }

        public DateTime DateAdded { get; set; }

        public Movie Movie { get; set; }

        public User User { get; set; }
    }
}
