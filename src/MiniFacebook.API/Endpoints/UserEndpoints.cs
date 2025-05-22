using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.API.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/users");

            group.RequireAuthorization();

            // Get a specific user
            group.MapGet("/{email}", async (string email, IUserRepository userRepository) =>
            {
                var user = await userRepository.GetByEmailAsync(email);

                if (user == null) return Results.NotFound();
                return Results.Ok(user);
            });
        }
    }

}
