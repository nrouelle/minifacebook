namespace MiniFacebook.Application.DTOs
{
    public record CreatePostDto(string Content, string? ImageUrl, Guid AuthorId);

}
