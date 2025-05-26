using MiniFacebook.Application.DTOs.Subscriptions;
using MiniFacebook.Application.Interfaces.Subscriptions;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Subscriptions
{
    public class GetPendingSubscriptions : IGetPendingSubscriptions
    {
        private readonly ISubscriptionRepository _repository;

        public GetPendingSubscriptions(ISubscriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SubscriptionDto>> ExecuteAsync(string userEmail)
        {
            var pending = await _repository.GetPendingSubscriptionsAsync(userEmail);
            return pending.Select(s => new SubscriptionDto
            {
                SubscriberEmail = s.SubscriberEmail,
                SubscribedToEmail = s.SubscribedToEmail
            }).ToList();
        }
    }
}
