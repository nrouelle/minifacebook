using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var postList = _context.Posts;
            return await Task.FromResult(postList
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .Include(p => p.Likes).ToList());
        }

        public Task<Post?> GetByIdAsync(Guid postId)
        {
            var post = _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(p => p.Id == postId);

            return post;
        }
    }
}
