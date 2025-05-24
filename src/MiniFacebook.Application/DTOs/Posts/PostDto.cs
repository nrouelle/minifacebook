namespace MiniFacebook.Application.DTOs.Posts
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public AuthorDto Author { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public PostDto(Guid id, string content, AuthorDto author, DateTime createdAt)
        {
            Id = id;
            Content = content;
            Author = author;
            CreatedAt = createdAt;
        }
    }
}
