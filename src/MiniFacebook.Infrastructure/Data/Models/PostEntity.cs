using System.ComponentModel.DataAnnotations;

namespace MiniFacebook.Infrastructure.Data.Models;

public class PostEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public UserEntity Author { get; set; }
    public string AuthorId { get; set; }

}
