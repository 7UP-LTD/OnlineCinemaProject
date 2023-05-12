using System;

namespace OnlineCinema.Data.Entities
{
    public class MovieTagEntity
    {
        public Guid TagId { get; set; }

        public TagsDictionaryEntity Tag { get; set; }

        public Guid MovieId { get; set; }

        public MovieEntity Movie { get; set; }
    }
}