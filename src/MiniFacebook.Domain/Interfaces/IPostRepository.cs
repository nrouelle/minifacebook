using MiniFacebook.Domain.Entities;

namespace MiniFacebook.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task AddAsync(Post post);
        Task DeleteAsync(Post post);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(Guid postId);
    }
}
