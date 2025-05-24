using MiniFacebook.Domain.Entities;

namespace MiniFacebook.Application.Interfaces.Users
{
    public interface IRegisterUser
    {
        Task<User> ExecuteAsync(string fullName, string email, string password);
    }
}