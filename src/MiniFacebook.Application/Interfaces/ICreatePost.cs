using MiniFacebook.Application.DTOs.Posts;

namespace MiniFacebook.Application.Interfaces
{
    public interface ICreatePost
    {
        Task<PostDto> ExecuteAsync(CreatePostDto dto);
    }
}