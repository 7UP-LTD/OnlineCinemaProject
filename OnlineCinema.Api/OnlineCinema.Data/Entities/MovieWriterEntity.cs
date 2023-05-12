using System;

namespace OnlineCinema.Data.Entities
{
    public class MovieWriterEntity : BaseEntity
    {
        public Guid WriterId { get; set; }
        public DicWriterEntity Writer { get; set; }

        public Guid MovieId { get; set; }
        public MovieEntity Movie { get; set; }
    }
}