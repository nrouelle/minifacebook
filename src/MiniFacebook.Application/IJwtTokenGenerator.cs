using MiniFacebook.Domain.Entities;

namespace MiniFacebook.Application
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }

}
