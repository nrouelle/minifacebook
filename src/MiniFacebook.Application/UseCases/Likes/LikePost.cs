using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Likes;

public class LikePost
{
    private readonly ILikeRepository _likeRepository;

    public LikePost(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public async Task ExecuteAsync(Guid postId, Guid userId)
    {
        var exists = await _likeRepository.ExistsAsync(postId, userId);
        if (exists)
            return; // éviter les doublons

        var like = new Like
        {
            Id = Guid.NewGuid(),
            PostId = postId,
            UserId = userId
        };

        await _likeRepository.AddAsync(like);
    }
}
