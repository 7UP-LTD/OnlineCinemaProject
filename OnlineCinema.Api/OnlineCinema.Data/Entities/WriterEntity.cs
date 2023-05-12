using System;

namespace OnlineCinema.Data.Entities
{
    public class WriterEntity
    {
        public Guid PersonId { get; set; }

        public PersonEntity Person { get; set; }
    }
}