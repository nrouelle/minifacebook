using Microsoft.EntityFrameworkCore;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;
using MiniFacebook.Infrastructure.Data;
using MiniFacebook.Infrastructure.Mappers;

namespace MiniFacebook.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Subscription?> GetSubscriptionAsync(string subscriberEmail, string subscribedToEmail)
        {
            var entity = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.SubscribedToEmail == subscribedToEmail
                    && s.SubscriberEmail == subscriberEmail);
            if (entity == null) {
                return null;
            }
            return SubscriptionMapper.ToDomain(entity);
        }

        public async Task SubscribeAsync(Subscription subscription)
        {
            var entity = SubscriptionMapper.ToEntity(subscription);
            await _context.Subscriptions.AddAsync(entity);
        }

        public bool SubscriptionExists(string subscriberEmail, string subscribedToEmail)
        {
            return _context.Subscriptions
                .Any(s => s.SubscribedToEmail == subscribedToEmail 
                    && s.SubscriberEmail == subscriberEmail);
        }

        public Task UpdateSubscriptionAsync(Subscription subscription)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Subscription>> GetPendingSubscriptionsAsync(string subscribedToEmail)
        {
            var entities = await _context.Subscriptions
                .Where(s => s.SubscribedToEmail == subscribedToEmail && !s.IsValidated)
                .ToListAsync();
            return entities.Select(SubscriptionMapper.ToDomain);
        }

    }
}
