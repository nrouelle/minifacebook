using MiniFacebook.Application.DTOs.Posts;

namespace MiniFacebook.Application.Interfaces
{
    public interface IGetAllPosts
    {
        Task<IEnumerable<PostDto>> ExecuteAsync();
    }
}
