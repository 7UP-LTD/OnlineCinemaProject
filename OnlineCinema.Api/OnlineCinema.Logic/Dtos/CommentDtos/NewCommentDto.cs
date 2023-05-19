using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Logic.Dtos.CommentDto
{
    public class NewCommentDto
    {
        public Guid UserId { get; set; }

        public Guid MovieId { get; set; }

        public string Text { get; set; } = null!;
    }
}
