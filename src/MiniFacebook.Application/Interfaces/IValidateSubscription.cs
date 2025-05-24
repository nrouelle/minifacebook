
namespace MiniFacebook.Application.Interfaces
{
    public interface IValidateSubscription
    {
        Task ExecuteAsync(string subscriberEmail, string subscribedToEmail);
    }
}