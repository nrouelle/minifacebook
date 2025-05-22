namespace MiniFacebook.Domain.Entities
{
    public class Post
    {
        public PostId Id { get; internal set; }
        public User Author { get; private set; }
        public string Content { get; internal set; } = string.Empty;
        public DateTime CreatedAt { get; internal set; }

        public Post(User author, string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentException("Content cannot be empty.", nameof(content));
            Id = new PostId(Guid.NewGuid());
            Author = author;
            Content = content;
        }

        public Post(PostId id, User author, string content, DateTime createdAt)
        {
            Id = id;
            Author = author;
            Content = content;
            CreatedAt = createdAt;
        }

        public void EditContent(string newContent)
        {
            if(string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("New content cannot be empty.", nameof(newContent));
            
            Content = newContent;
        }
    }

    public sealed class PostId
    {
        public Guid Value { get; }

        public PostId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("PostId cannot be null or empty", nameof(value));

            Value = value;
        }

        public override string ToString() => Value.ToString();

        public override bool Equals(object obj) =>
            obj is PostId other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
