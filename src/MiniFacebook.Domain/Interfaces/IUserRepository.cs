using MiniFacebook.Domain.Entities;

namespace MiniFacebook.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task CreateAsync(User user);
    Task<bool> ExistsAsync(string userEmail);
}
