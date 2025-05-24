using MiniFacebook.Application;
using MiniFacebook.Application.DTOs.Auth;
using MiniFacebook.Application.Interfaces.Users;

namespace MiniFacebook.API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth");

            group.MapPost("/register", async (UserRegisterDto dto, IRegisterUser registerUser) =>
            {
                var userCreated = await registerUser.ExecuteAsync(dto.FullName, dto.Email, dto.Password);

                return Results.Ok(new { userCreated.FullName, userCreated.Email });
            });

            group.MapPost("/login", async (
                UserLoginDto dto,
                ILogUserIn logUserIn,
                IJwtTokenGenerator tokenGen) =>
            {
                try
                {
                    var response = await logUserIn.ExecuteAsync(dto);

                    return Results.Ok(new { FullName = response.FullName, Email = response.Email, Token = response.Token });
                }
                catch (Exception)
                {
                    return Results.NotFound();
                }
            });
        }
    }

}
