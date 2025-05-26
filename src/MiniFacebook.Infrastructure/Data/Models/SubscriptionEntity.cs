using System.ComponentModel.DataAnnotations;

namespace MiniFacebook.Infrastructure.Data.Models;

public class SubscriptionEntity
{
    [Required]
    public string SubscriberEmail { get; set; }
    
    [Required]
    public string SubscribedToEmail { get; set; }
    
    [Required]
    public bool IsValidated { get; set; }

    public UserEntity Subscriber { get; set; }
    public UserEntity SubscribedTo { get; set; }

    public SubscriptionEntity() { }
}
