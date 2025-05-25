using MiniFacebook.Application.DTOs.Posts;

namespace MiniFacebook.Application.Interfaces.Posts
{
    public interface ICreatePost
    {
        Task<PostDto> ExecuteAsync(CreatePostDto dto);
    }
}