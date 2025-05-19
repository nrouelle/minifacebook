using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniFacebook.Application.DTOs;
using MiniFacebook.Infrastructure.Data;

namespace MiniFacebook.API.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/users");

            // Get a specific user
            group.MapGet("/{id:guid}", async (Guid id, AppDbContext db) =>
            {
                var user = await db.Users
                    .Include(u => u.Posts)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null) return Results.NotFound();
                return Results.Ok(user);
            });

            // Update user
            group.MapPut("/{id:guid}", async (Guid id, UpdateUserDto dto, AppDbContext db) =>
            {
                var user = await db.Users.FindAsync(id);
                if (user == null) return Results.NotFound();

                user.FullName = dto.FullName;
                user.Bio = dto.Bio;
                user.ProfileImageUrl = dto.ProfileImageUrl;

                await db.SaveChangesAsync();
                return Results.Ok(user);
            });

            // Search users
            group.MapGet("/search", async ([FromQuery] string query, AppDbContext db) =>
            {
                var users = await db.Users
                    .Where(u => u.FullName.Contains(query))
                    .ToListAsync();

                return Results.Ok(users);
            });
        }
    }

}
