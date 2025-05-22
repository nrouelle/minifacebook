using MiniFacebook.Domain.Entities;
using MiniFacebook.Infrastructure.Data.Models;

namespace MiniFacebook.Infrastructure.Mappers;

public static class PostMapper
{
    public static PostEntity ToEntity(Post domain)
    {
        return new PostEntity
        {
            Id = domain.Id,
            AuthorId = domain.AuthorId,
            Content = domain.Content,
            CreatedAt = domain.CreatedAt
        };
    }

    public static Post ToDomain(PostEntity entity)
    {
        return new Post(
            entity.Id,
            entity.AuthorId,
            entity.Author?.UserName ?? string.Empty,
            entity.Content,
            entity.CreatedAt
        );
    }
}
