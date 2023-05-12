
namespace OnlineCinema.Logic.Models
{
    public class MovieTag
    {
        public int MovieTagId { get; set; }

        public int MovieId { get; set; }

        public int TagId { get; set; }

        public Movie Movie { get; set; }

        public Tag Tag { get; set; }
    }
}
