using MiniFacebook.Domain.Entities;
using MiniFacebook.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFacebook.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
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
    }
}
