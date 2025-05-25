namespace MiniFacebook.Application.Interfaces.Subscriptions
{
    public interface IValidateSubscription
    {
        Task ExecuteAsync(string subscriberEmail, string subscribedToEmail);
    }
}