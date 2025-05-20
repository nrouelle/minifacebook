using MiniFacebook.Domain.Entities;

namespace MiniFacebook.API
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }

}
