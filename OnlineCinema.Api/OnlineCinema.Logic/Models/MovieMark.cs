
namespace OnlineCinema.Logic.Models
{
    public class MovieMark
    {
        public int MovieMarId { get; set; }

        public int MovieId { get; set; }

        public int UserId { get; set; }

        public int Mark { get; set; }

        public DateTime DateCreated { get; set; }

        public Movie Movie { get; set; }

        public User User { get; set; }
    }
}
