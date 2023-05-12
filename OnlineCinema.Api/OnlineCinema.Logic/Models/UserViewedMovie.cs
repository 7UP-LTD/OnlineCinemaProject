
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineCinema.Logic.Models
{
    public class UserViewedMovie
    {
        public int UserViewedMovieId { get; set; }

        public int MovieId { get; set; }

        public int UserId { get; set; }

        public DateTime DateViewed { get; set; }

        public Movie Movie { get; set; }

        public User User { get; set; }
    }
}
