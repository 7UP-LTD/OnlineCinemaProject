using System;

namespace OnlineCinema.Data.Entities
{
    public class DicWriterEntity : BaseEntity
    {
        public Guid PersonId { get; set; }
        public PersonEntity Person { get; set; }
    }
}