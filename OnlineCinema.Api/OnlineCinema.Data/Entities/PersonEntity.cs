using System;

namespace OnlineCinema.Data.Entities
{
    public class PersonEntity : BaseEntity
    {
        public string FirstName { get; set; }

        public string? Surname { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}