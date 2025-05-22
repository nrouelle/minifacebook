using MiniFacebook.Application;
using MiniFacebook.Application.DTOs.Auth;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth");

            group.MapPost("/register", async (UserRegisterDto dto, IUserRepository userRepository) =>
            {
                var user = new User
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
                };

                await userRepository.CreateAsync(user);

                return Results.Ok(new { user.Id, user.FullName, user.Email });
            });

            group.MapPost("/login", async (
                UserLoginDto dto, 
                IUserRepository userRepository,
                IJwtTokenGenerator tokenGen) =>
            {
                var user = await userRepository.GetByEmailAsync(dto.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                    return Results.Unauthorized();

                var token = tokenGen.GenerateToken(user);
                return Results.Ok(new { token });
            });
        }
    }

}
