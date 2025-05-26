using MiniFacebook.Domain.Entities;
using MiniFacebook.Infrastructure.Data.Models;

namespace MiniFacebook.Infrastructure.Mappers;

public static partial class PostMapper
{
    public static PostEntity ToEntity(Post post)
    {
        return new PostEntity
        {
            Id = post.Id.Value,
            AuthorId = post.Author.Email,
            Content = post.Content,
            CreatedAt = post.CreatedAt
        };
    }

    public static Post ToDomain(PostEntity entity)
    {
        return new Post(
            new PostId(entity.Id),
            UserMapper.ToDomain(entity.Author),
            entity.Content,
            entity.CreatedAt
        );
    }
}
