namespace MiniFacebook.Application.DTOs
{
    public record CreateCommentDto(string Text, Guid AuthorId);
}
