using System;

namespace OnlineCinema.Logic.Models
{
    public class DicWriterDto
    {
        public Guid PersonId { get; set; }
        public PersonDto Person { get; set; }
    }
}