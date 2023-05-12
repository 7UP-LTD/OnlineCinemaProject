
namespace OnlineCinema.Logic.Models
{
    public class UserFavoriteTag
    {
        public int UserFavoriteTagId { get; set; }

        public int TagId { get; set; }

        public int UserId { get; set; }

        public DateTime DateAdded { get; set; }

        public Tag Tag { get; set; }

        public User User { get; set; }
    }
}
