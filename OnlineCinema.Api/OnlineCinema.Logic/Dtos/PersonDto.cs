using System;

namespace OnlineCinema.Logic.Models
{
    public class PersonDto
    {
        public string FirstName { get; set; }
        public string? Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Biography { get; set; }
    }
}