using System;

namespace OnlineCinema.Data.Entities
{
    public class DirectorEntity
    {
        public Guid PersonId { get; set; }

        public PersonEntity Person { get; set; }
    }
}