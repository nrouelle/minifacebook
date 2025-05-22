using System.ComponentModel.DataAnnotations;

namespace MiniFacebook.Infrastructure.Data.Models
{
    public class UserEntity
    {
        public UserEntity(string fullName, string email, string passwordHash, DateTime createdAt)
        {
            FullName = fullName;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = createdAt;
        }

        public UserEntity()
        {
            
        }

        [Key]
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Navigation
        public ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set;  }
        //public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
        //public ICollection<LikeEntity> Likes { get; set; } = new List<LikeEntity>();
    }
}