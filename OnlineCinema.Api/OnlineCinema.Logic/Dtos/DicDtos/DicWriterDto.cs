using System;
using OnlineCinema.Logic.Models;

namespace OnlineCinema.Logic.Dtos.DicDtos
{
    public class DicWriterDto
    {
        public Guid PersonId { get; set; }
        public PersonDto Person { get; set; }
    }
}