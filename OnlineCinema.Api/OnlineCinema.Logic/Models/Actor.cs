
namespace OnlineCinema.Logic.Models
{
    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
