using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;
using MiniFacebook.Infrastructure.Data;

namespace MiniFacebook.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }
    }
}
