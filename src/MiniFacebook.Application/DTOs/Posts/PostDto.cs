namespace MiniFacebook.Application.DTOs.Posts
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
