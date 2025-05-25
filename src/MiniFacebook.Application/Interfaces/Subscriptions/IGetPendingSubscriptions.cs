using MiniFacebook.Application.DTOs.Subscriptions;

namespace MiniFacebook.Application.Interfaces.Subscriptions
{
    public interface IGetPendingSubscriptions
    {
        Task<List<SubscriptionDto>> ExecuteAsync(string userEmail);
    }
}
