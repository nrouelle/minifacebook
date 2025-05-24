using MiniFacebook.Application.DTOs.Posts;

namespace MiniFacebook.Application.Interfaces
{
    public interface IGetPost
    {
        Task<PostDto?> ExecuteAsync(Guid postId);
    }
}