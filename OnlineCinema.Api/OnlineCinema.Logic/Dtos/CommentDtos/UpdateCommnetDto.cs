namespace OnlineCinema.Logic.Dtos.CommentDtos
{
    public class UpdateCommnetDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; } = null!;
    }
}