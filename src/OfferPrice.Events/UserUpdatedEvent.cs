
namespace OfferPrice.Events;

public class UserUpdatedEvent : Event
{
    public UserUpdatedEvent(User user)
    {
        User = user;
    }

    public User User { get; set; }
}

