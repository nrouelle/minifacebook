using MiniFacebook.Application.DTOs.Subscriptions;

namespace MiniFacebook.Application.Interfaces.Subscriptions
{
    public interface ISubscribeUser
    {
        Task ExecuteAsync(SubscriptionDto dto);
    }
}