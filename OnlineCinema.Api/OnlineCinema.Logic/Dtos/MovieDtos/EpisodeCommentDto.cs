using System;
using OnlineCinema.Logic.Models;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class EpisodeCommentDto
    {
        public string Text { get; set; }
        
        public Guid EpisodeId { get; set; }
        public MovieEpisodeDto Episode { get; set; }
    }
}