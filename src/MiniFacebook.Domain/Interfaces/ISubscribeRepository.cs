using MiniFacebook.Domain.Entities;

namespace MiniFacebook.Domain.Interfaces
{
    public interface ISubscribeRepository
    {
        Task SubscribeAsync(Subscription subscription);
        bool SubscriptionExists(string subscriberEmail, string subscribedToEmail);
    }
}
