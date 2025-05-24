using MiniFacebook.Application.DTOs.Auth;

namespace MiniFacebook.Application.Interfaces.Users
{
    public interface ILogUserIn
    {
        Task<LoginResponse> ExecuteAsync(UserLoginDto dto);
    }
}
