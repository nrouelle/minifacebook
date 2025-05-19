using Microsoft.EntityFrameworkCore;
using MiniFacebook.Application.DTOs;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Infrastructure.Data;

namespace MiniFacebook.API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth");

            group.MapPost("/register", async (UserRegisterDto dto, AppDbContext db) =>
            {
                var user = new User
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
                };

                db.Users.Add(user);
                await db.SaveChangesAsync();

                return Results.Ok(new { user.Id, user.FullName, user.Email });
            });

            group.MapPost("/login", async (UserLoginDto dto, AppDbContext db) =>
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                    return Results.Unauthorized();

                // TODO: JWT token
                return Results.Ok(new { user.Id, user.FullName, user.Email });
            });
        }
    }

}
