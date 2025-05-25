using MiniFacebook.Application.DTOs.Posts;

namespace MiniFacebook.Application.Interfaces.Posts
{
    public interface IGetAllPosts
    {
        Task<IEnumerable<PostDto>> ExecuteAsync();
    }
}
