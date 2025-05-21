using Microsoft.EntityFrameworkCore;
using MiniFacebook.Domain.Interfaces;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Infrastructure.Data;

namespace MiniFacebook.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
