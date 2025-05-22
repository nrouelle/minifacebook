namespace MiniFacebook.Infrastructure.Data.Models
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Navigation
        public ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();
        //public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
        //public ICollection<LikeEntity> Likes { get; set; } = new List<LikeEntity>();
    }
}