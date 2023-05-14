using System;

namespace OnlineCinema.Logic.Models
{
    public class DicDirectorDto
    {
        public Guid PersonId { get; set; }
        public PersonDto Person { get; set; }
    }
}