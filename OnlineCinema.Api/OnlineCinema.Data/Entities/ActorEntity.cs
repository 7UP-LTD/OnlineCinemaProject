using System;

namespace OnlineCinema.Data.Entities
{
    public class ActorEntity : BaseEntity
    {
        public Guid PersonId { get; set; }

        public PersonEntity Person { get; set; }
    }
}