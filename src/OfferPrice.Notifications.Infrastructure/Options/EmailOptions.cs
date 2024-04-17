namespace OfferPrice.Notifications.Infrastructure.Options;

public class EmailOptions
{
    public string ServerDomain { get; set; }

    public string AppName { get; set; }

    public string Password { get; set; }

    public int Port { get; set; }
}
