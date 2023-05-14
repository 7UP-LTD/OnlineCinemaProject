using System;

namespace OnlineCinema.Logic.Models
{
    public class DicActorDto
    {
        public Guid PersonId { get; set; }
        public PersonDto Person { get; set; }
    }
}