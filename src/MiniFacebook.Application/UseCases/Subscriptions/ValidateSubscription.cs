using MiniFacebook.Application.Interfaces;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Subscriptions
{
    public class ValidateSubscription: IValidateSubscription
    {
        private readonly ISubscriptionRepository _subscribeRepository;

        public ValidateSubscription(ISubscriptionRepository subscribeRepository)
        {
            _subscribeRepository = subscribeRepository;
        }

        public async Task ExecuteAsync(string subscriberEmail, string subscribedToEmail)
        {
            var subscription = await _subscribeRepository.GetSubscriptionAsync(subscriberEmail, subscribedToEmail);

            if (subscription == null)
            {
                throw new InvalidOperationException("Subscription does not exist.");
            }

            if (subscription.IsValidated)
            {
                throw new InvalidOperationException("Subscription is already validated.");
            }

            subscription.Validate();
            await _subscribeRepository.UpdateSubscriptionAsync(subscription);
        }
    }
}
