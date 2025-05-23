using Microsoft.EntityFrameworkCore;
using MiniFacebook.Infrastructure.Data.Models;

namespace MiniFacebook.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<PostEntity> Posts => Set<PostEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relations, Indexes, etc.
        
    }
}
