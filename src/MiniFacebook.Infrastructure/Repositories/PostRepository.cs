using Microsoft.EntityFrameworkCore;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;
using MiniFacebook.Infrastructure.Data;
using MiniFacebook.Infrastructure.Mappers;

namespace MiniFacebook.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;

    public PostRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Post?> GetByIdAsync(Guid id)
    {
        var entity = await _context.Posts.FindAsync(id);
        return entity is null ? null : PostMapper.ToDomain(entity);
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        var entities = await _context.Posts.Include(p => p.Author).ToListAsync();
        return entities.Select(PostMapper.ToDomain);
    }

    public async Task AddAsync(Post post)
    {
        var entity = PostMapper.ToEntity(post);
        _context.Posts.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Post post)
    {
        var entity = PostMapper.ToEntity(post);
        _context.Posts.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
