using MiniFacebook.Application.DTOs.Subscriptions;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.API.Endpoints
{
    public static class SubscriptionEndpoints
    {
        public static void MapSubscriptionEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/subscribe");

            group.RequireAuthorization();

            group.MapPost("/", async (SubscriptionDto dto, ISubscribeRepository subscribeRepository, IUserRepository userRepository) =>
            {
                var userToSubscribeExists = await userRepository.GetByEmailAsync(dto.SubscribedToEmail);
                if (userToSubscribeExists == null)
                    return Results.NotFound("User not found");

                var subscription = new Subscription(dto.SubscriberEmail, dto.SubscribedToEmail);

                await subscribeRepository.SubscribeAsync(subscription);

                return Results.Created();
            });
        }
    }
}
