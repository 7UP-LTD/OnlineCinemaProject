
namespace OnlineCinema.Logic.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime DateRegistered { get; set; }

        public ICollection<MovieComments> MovieComment { get; set; }
    }
}
