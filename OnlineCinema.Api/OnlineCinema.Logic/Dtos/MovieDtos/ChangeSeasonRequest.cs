using System;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class ChangeSeasonRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime ReleaseDate { get; set; }
        
        public Guid MovieId { get; set; }
    }
}