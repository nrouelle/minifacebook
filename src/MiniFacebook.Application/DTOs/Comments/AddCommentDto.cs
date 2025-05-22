namespace MiniFacebook.Application.DTOs.Comments;

public record AddCommentDto(Guid PostId, Guid UserId)
{
    public string Content { get; set; } = string.Empty;
}
