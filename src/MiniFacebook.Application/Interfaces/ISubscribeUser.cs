using MiniFacebook.Application.DTOs.Subscriptions;

namespace MiniFacebook.Application.Interfaces
{
    public interface ISubscribeUser
    {
        Task ExecuteAsync(SubscriptionDto dto);
    }
}