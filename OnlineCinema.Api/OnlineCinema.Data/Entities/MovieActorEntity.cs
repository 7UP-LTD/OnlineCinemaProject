using System;

namespace OnlineCinema.Data.Entities
{
    public class MovieActorEntity : BaseEntity
    {
        public Guid ActorId { get; set; }
        public DicActorEntity Actor { get; set; }

        public Guid MovieId { get; set; }
        public MovieEntity Movie { get; set; }
    }
}