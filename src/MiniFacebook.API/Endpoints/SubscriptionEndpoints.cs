using MiniFacebook.Application.DTOs.Subscriptions;
using MiniFacebook.Application.Interfaces;
using MiniFacebook.Application.Interfaces.Users;
using MiniFacebook.Application.UseCases.Subscriptions;
using MiniFacebook.Domain.Entities;

namespace MiniFacebook.API.Endpoints
{
    public static class SubscriptionEndpoints
    {
        public static void MapSubscriptionEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/subscribe");

            group.RequireAuthorization();

            group.MapPost("/", async (SubscriptionDto dto, ISubscribeUser subscribeToUser, ICheckUserExists checkUserExists) =>
            {

                var userToSubscribeExists = await checkUserExists.ExecuteAsync(dto.SubscribedToEmail);
                if (!userToSubscribeExists)
                    return Results.NotFound("User not found");

                await subscribeToUser.ExecuteAsync(dto);

                return Results.Created();
            });

            app.MapPost("/subscriptions/validate", async (ValidateSubscriptionDto dto, ValidateSubscription validateSubscription) =>
            {
                try
                {
                    await validateSubscription.ExecuteAsync(dto.SubscriberEmail, dto.SubscribedToEmail);
                    return Results.Ok(new { message = "Subscription validated." });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
        .WithTags("Subscriptions")
        .WithName("ValidateSubscription");
        }
    }
}
