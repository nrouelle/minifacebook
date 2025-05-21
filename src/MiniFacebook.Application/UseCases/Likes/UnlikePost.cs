using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Likes;

public class UnlikePost
{
    private readonly ILikeRepository _likeRepository;

    public UnlikePost(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public async Task ExecuteAsync(Guid postId, Guid userId)
    {
        await _likeRepository.RemoveAsync(postId, userId);
    }
}
