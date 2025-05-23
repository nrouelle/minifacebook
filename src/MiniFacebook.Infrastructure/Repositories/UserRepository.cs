using Microsoft.EntityFrameworkCore;
using MiniFacebook.Domain.Interfaces;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Infrastructure.Data;
using MiniFacebook.Infrastructure.Mappers;

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
            var entity = UserMapper.ToEntity(user);
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(string userEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
            if(user == null) return null;

            return UserMapper.ToDomain(user);
        }
    }
}
