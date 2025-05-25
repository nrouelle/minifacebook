using MiniFacebook.Application.DTOs.Subscriptions;
using MiniFacebook.Application.Interfaces.Subscriptions;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;

namespace MiniFacebook.Application.UseCases.Subscriptions
{
    public class SubscribeUser: ISubscribeUser
    {
        private readonly ISubscriptionRepository subscribeRepository;

        public SubscribeUser(ISubscriptionRepository subscribeRepository)
        {
            this.subscribeRepository = subscribeRepository;
        }

        public async Task ExecuteAsync(SubscriptionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.SubscriberEmail) || string.IsNullOrWhiteSpace(dto.SubscribedToEmail))
                throw new ArgumentException("Emails must not be empty.");

            if (dto.SubscriberEmail == dto.SubscribedToEmail)
                throw new InvalidOperationException("Cannot subscribe to oneself.");

            var alreadyExists = subscribeRepository.SubscriptionExists(dto.SubscriberEmail, dto.SubscribedToEmail);
            if (!alreadyExists)
            {
                Subscription subscription = new Subscription(dto.SubscriberEmail, dto.SubscribedToEmail);

                await subscribeRepository.SubscribeAsync(subscription);
            }
        }
    }
}
