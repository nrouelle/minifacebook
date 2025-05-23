namespace MiniFacebook.Domain.Entities
{
    public class Subscription
    {
        public Subscription(string subscriberEmail, string subscribedToEmail)
        {
            SubscriberEmail = subscriberEmail;
            SubscribedToEmail = subscribedToEmail;
        }

        public string SubscriberEmail { get; set; }
        public string SubscribedToEmail { get; set; }
        public bool IsValidated { get; set; } = false;
    }
}
