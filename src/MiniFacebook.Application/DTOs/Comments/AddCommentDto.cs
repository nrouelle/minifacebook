namespace MiniFacebook.Application.DTOs;

public class AddCommentDto
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; } = string.Empty;
}
