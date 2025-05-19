using MiniFacebook.Domain.Entities;

namespace MiniFacebook.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task CreateAsync(User user);
}
