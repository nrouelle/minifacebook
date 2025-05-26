using Microsoft.EntityFrameworkCore;
using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;
using MiniFacebook.Infrastructure.Data;
using MiniFacebook.Infrastructure.Data.Models;

namespace MiniFacebook.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Subscription?> GetSubscriptionAsync(string subscriberEmail, string subscribedToEmail)
        {
            throw new NotImplementedException();
        }

        public Task SubscribeAsync(Subscription subscription)
        {
            throw new NotImplementedException();
        }

        public bool SubscriptionExists(string subscriberEmail, string subscribedToEmail)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSubscriptionAsync(object subscription)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Subscription>> GetPendingSubscriptionsAsync(string subscribedToEmail)
        {
            return await _context.Subscriptions
                .Where(s => s.SubscribedToEmail == subscribedToEmail && !s.IsValidated)
                .ToListAsync();
        }

    }
}
