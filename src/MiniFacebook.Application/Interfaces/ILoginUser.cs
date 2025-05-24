using MiniFacebook.Application.DTOs.Auth;

namespace MiniFacebook.Application.Interfaces
{
    public interface ILoginUser
    {
        Task<LoginResponse> ExecuteAsync(LoginRequest dto);
    }
}