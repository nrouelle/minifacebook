namespace MiniFacebook.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; internal set; }
        public Guid AuthorId { get; internal set; }
        public string Content { get; internal set; } = string.Empty;
        public DateTime CreatedAt { get; internal set; }

        public Post()
        {
        }

        public Post(Guid id, Guid authorId, string content, DateTime createdAt)
        {
            Id = id;
            AuthorId = authorId;
            Content = content;
            CreatedAt = createdAt;
        }

        public void Create(Guid authorId, string content)
        {
            if(authorId == Guid.Empty)
                throw new ArgumentException("AuthorId cannot be empty.", nameof(authorId));
            if(string.IsNullOrEmpty(content))
                throw new ArgumentException("Content cannot be empty.", nameof(content));

            this.Id = Guid.NewGuid();
            this.AuthorId = authorId;
            this.Content = content;
            this.CreatedAt = DateTime.UtcNow;
        }

        public void EditContent(string newContent)
        {
            if(string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("New content cannot be empty.", nameof(newContent));
            
            Content = newContent;
        }
    }
}
