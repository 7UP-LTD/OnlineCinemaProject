
namespace OnlineCinema.Logic.Models
{
    public class Actor
    {
        public int ActorId { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
