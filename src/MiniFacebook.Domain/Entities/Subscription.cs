namespace MiniFacebook.Domain.Entities
{
    public class Subscription
    {
        public Subscription(string subscriberEmail, string subscribedToEmail)
        {
            SubscriberEmail = subscriberEmail;
            SubscribedToEmail = subscribedToEmail;
        }

        public string SubscriberEmail { get; set; } = string.Empty;
        public string SubscribedToEmail { get; set; } = string.Empty;
        public bool IsValidated { get; set; } = false;

        public void Validate()
        {
            IsValidated = true;
        }
    }
}
