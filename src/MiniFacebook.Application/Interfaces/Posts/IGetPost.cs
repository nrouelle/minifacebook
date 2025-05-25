using MiniFacebook.Application.DTOs.Posts;

namespace MiniFacebook.Application.Interfaces.Posts
{
    public interface IGetPost
    {
        Task<PostDto?> ExecuteAsync(Guid postId);
    }
}