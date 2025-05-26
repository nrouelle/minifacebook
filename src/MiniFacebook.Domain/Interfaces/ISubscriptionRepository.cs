using MiniFacebook.Domain.Entities;

namespace MiniFacebook.Domain.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetPendingSubscriptionsAsync(string userEmail);
        Task<Subscription?> GetSubscriptionAsync(string subscriberEmail, string subscribedToEmail);
        Task SubscribeAsync(Subscription subscription);
        bool SubscriptionExists(string subscriberEmail, string subscribedToEmail);
        Task UpdateSubscriptionAsync(object subscription);
    }
}
