namespace OfferPrice.Events;

public class UserCreatedEvent : Event
{
    public UserCreatedEvent(User user)
    {
        User = user;
    }

    public User User { get; set; }
}