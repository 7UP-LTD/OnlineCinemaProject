using System;

namespace OnlineCinema.Data.Entities
{
    public class MovieEpisodeEntity : BaseEntity
    {
        public string Name { get; set; }

        public string? Description { get; set; }
        
        public DateTime DateAdded { get; set; }
       
        public string ContentUrl { get; set; }
        
        public Guid SeasonId { get; set; }
    }
}