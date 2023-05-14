using System;

namespace OnlineCinema.Logic.Models
{
    public class MovieCommentDto
    {
        public string Text { get; set; }
        
        public Guid MovieId { get; set; }
        public MovieDto Movie { get; set; }
    }
}