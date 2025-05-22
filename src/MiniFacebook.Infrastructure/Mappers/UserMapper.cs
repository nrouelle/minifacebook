using MiniFacebook.Domain.Entities;
using MiniFacebook.Infrastructure.Data.Models;

namespace MiniFacebook.Infrastructure.Mappers;

public static class UserMapper
{
    public static UserEntity ToEntity(User user)
    {
        return new UserEntity(
            user.FullName,
            user.Email,
            user.PasswordHash,
            user.CreatedAt
        );
    }

    public static User ToDomain(UserEntity entity)
    {
        if(entity == null)
            throw new ArgumentNullException(nameof(entity));
        return new User (
            entity.FullName,
            entity.Email,
            entity.PasswordHash
        );
    }
}
