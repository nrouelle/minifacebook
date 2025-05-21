using MiniFacebook.Domain.Entities;

namespace MiniFacebook.Domain.Interfaces
{
    public interface ILikeRepository
    {
        Task<bool> ExistsAsync(Guid postId, Guid userId);
        Task AddAsync(Like like);
        Task RemoveAsync(Guid postId, Guid userId);
    }
}
