using System;

namespace OnlineCinema.Data.Entities
{
    public class DicDirectorEntity : BaseEntity
    {
        public Guid PersonId { get; set; }
        public PersonEntity Person { get; set; }
    }
}