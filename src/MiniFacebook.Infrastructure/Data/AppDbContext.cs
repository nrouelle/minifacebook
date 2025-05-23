using Microsoft.EntityFrameworkCore;
using MiniFacebook.Infrastructure.Data.Models;

namespace MiniFacebook.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<PostEntity> Posts => Set<PostEntity>();

    public DbSet<SubscriptionEntity> Subscriptions => Set<SubscriptionEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>().HasKey(s => s.Email);

        modelBuilder.Entity<SubscriptionEntity>()
            .HasKey(s => new { s.SubscriberEmail, s.SubscribedToEmail });

        modelBuilder.Entity<SubscriptionEntity>()
        .HasOne(s => s.Subscriber)
        .WithMany() // ou `.WithMany(u => u.Subscriptions)` si tu veux une collection
        .HasForeignKey(s => s.SubscriberEmail)
        .HasPrincipalKey(u => u.Email)  // <- lien vers le champ Email du User
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SubscriptionEntity>()
            .HasOne(s => s.SubscribedTo)
            .WithMany()
            .HasForeignKey(s => s.SubscribedToEmail)
            .HasPrincipalKey(u => u.Email)
            .OnDelete(DeleteBehavior.Restrict);

    }

    public void Seed()
    {
        if (Users.Any() || Posts.Any()) return;

        var users = new List<UserEntity>
        {
            new UserEntity { FullName = "alice", PasswordHash = BCrypt.Net.BCrypt.HashPassword("123"), Email = "alice@example.com" },
            new UserEntity { FullName = "bob", PasswordHash = BCrypt.Net.BCrypt.HashPassword("123"), Email = "bob@example.com" },
            new UserEntity { FullName = "carol", PasswordHash = BCrypt.Net.BCrypt.HashPassword("123"), Email = "carol@example.com" }
        };

        Users.AddRange(users);
        SaveChanges();

        var posts = users.SelectMany(user => new[]
        {
            new PostEntity { Id = Guid.NewGuid(), AuthorId = user.Email, Content = $"Post 1 by {user.FullName}", CreatedAt = DateTime.UtcNow },
            new PostEntity { Id = Guid.NewGuid(), AuthorId = user.Email, Content = $"Post 2 by {user.FullName}", CreatedAt = DateTime.UtcNow },
            new PostEntity { Id = Guid.NewGuid(), AuthorId = user.Email, Content = $"Post 3 by {user.FullName}", CreatedAt = DateTime.UtcNow },
            new PostEntity { Id = Guid.NewGuid(), AuthorId = user.Email, Content = $"Post 4 by {user.FullName}", CreatedAt = DateTime.UtcNow },
        });

        Posts.AddRange(posts);
        SaveChanges();
    }
}
