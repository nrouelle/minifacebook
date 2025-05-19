using Microsoft.EntityFrameworkCore;
using MiniFacebook.Domain;
using MiniFacebook.Domain.Entities;
using System.Collections.Generic;

namespace MiniFacebook.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
}
