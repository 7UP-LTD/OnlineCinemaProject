using System;
using OnlineCinema.Data.Enums;

namespace OnlineCinema.Data.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string? Name { get; set; }
        
        public string? Surname { get; set; }
        
        public string? Icon { get; set; }
        
        public Role Role { get; set; }
    }
}