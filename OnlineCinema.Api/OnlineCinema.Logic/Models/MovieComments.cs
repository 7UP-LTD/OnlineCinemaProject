
using System.ComponentModel.DataAnnotations;

namespace OnlineCinema.Logic.Models
{
    public class MovieComments
    {
        public int IdMovieComments { get; set; }

        public int MovieId { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public int Rating { get; set; }
    }
}
