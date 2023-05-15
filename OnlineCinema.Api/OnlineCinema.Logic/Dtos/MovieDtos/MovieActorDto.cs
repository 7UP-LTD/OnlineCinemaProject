using System;
using OnlineCinema.Logic.Dtos.DicDtos;
using OnlineCinema.Logic.Models;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class MovieActorDto
    {
        public Guid ActorId { get; set; }
        public DicActorDto Actor { get; set; }

        public Guid MovieId { get; set; }
    }
}