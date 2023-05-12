
namespace OnlineCinema.Logic.Models
{
    public class Director
    {
        public int DirectorId { get; set; }

        public string SurName { get; set; }
        public string ForeName { get; set; }

        public DateTime BirthDate { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
