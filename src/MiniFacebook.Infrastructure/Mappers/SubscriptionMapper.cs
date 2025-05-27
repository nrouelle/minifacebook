using MiniFacebook.Domain.Entities;
using MiniFacebook.Infrastructure.Data.Models;

namespace MiniFacebook.Infrastructure.Mappers;

public static class SubscriptionMapper
{
    public static SubscriptionEntity ToEntity(Subscription subscription)
    {
        return new SubscriptionEntity
        {
            SubscriberEmail = subscription.SubscriberEmail,
            SubscribedToEmail = subscription.SubscribedToEmail,
            IsValidated = subscription.IsValidated,
        };
    }

    public static Subscription ToDomain(SubscriptionEntity entity)
    {
        return new Subscription(entity.SubscriberEmail, entity.SubscribedToEmail);
    }
}
