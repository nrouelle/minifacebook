namespace MiniFacebook.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; internal set; }
        public Guid AuthorId { get; private set; }
        public string AuthorName { get; private set; } = string.Empty;

        public string Content { get; internal set; } = string.Empty;
        public DateTime CreatedAt { get; internal set; }

        public Post()
        {
        }

        public Post(Guid authorId, string content)
        {
            if (authorId == Guid.Empty)
                throw new ArgumentException("AuthorId cannot be empty.", nameof(authorId));
            if (string.IsNullOrEmpty(content))
                throw new ArgumentException("Content cannot be empty.", nameof(content));

            AuthorId = authorId;
            Content = content;
        }

        public Post(Guid id, Guid authorId, string authorName, string content, DateTime createdAt)
        {
            Id = id;
            AuthorId = authorId;
            AuthorName = authorName;
            Content = content;
            CreatedAt = createdAt;
        }

        public void Create()
        {
            if (Id != Guid.Empty)
                throw new InvalidOperationException("Post has already been created.");
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public void EditContent(string newContent)
        {
            if(string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("New content cannot be empty.", nameof(newContent));
            
            Content = newContent;
        }
    }
}
