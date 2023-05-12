using System;

namespace OnlineCinema.Data.Entities
{
    public class MovieTagEntity : BaseEntity
    {
        public Guid TagId { get; set; }
        public DicTagEntity Tag { get; set; }

        public Guid MovieId { get; set; }
        public MovieEntity Movie { get; set; }
    }
}