using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniFacebook.Infrastructure.Data.Models;

public class PostEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    [ForeignKey(nameof(AuthorId))]
    public UserEntity Author { get; set; }
    [Required]
    public string AuthorId { get; set; }

    public PostEntity()
    {
        
    }
}
