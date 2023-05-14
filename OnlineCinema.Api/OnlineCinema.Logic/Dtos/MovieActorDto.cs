using System;

namespace OnlineCinema.Logic.Models
{
    public class MovieActorDto
    {
        public Guid ActorId { get; set; }
        public DicActorDto Actor { get; set; }

        public Guid MovieId { get; set; }
        public MovieDto Movie { get; set; }
    }
}