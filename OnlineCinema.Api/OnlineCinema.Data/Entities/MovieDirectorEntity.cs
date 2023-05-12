using System;

namespace OnlineCinema.Data.Entities
{
    public class MovieDirectorEntity : BaseEntity
    {
        public Guid DirectorId { get; set; }
        public DicDirectorEntity Director { get; set; }

        public Guid MovieId { get; set; }
        public MovieEntity Movie { get; set; }
    }
}