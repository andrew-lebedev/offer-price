using OfferPrice.Common;
using OfferPrice.Notifications.Application.Models;

namespace OfferPrice.Notifications.Api.Models;

public class NotificationResponse
{
    public NotificationResponse(PageResult<Notification> pageResult)
    {
        Notifications = pageResult.Items;
        Total = pageResult.Total;
        Page = pageResult.Page;
        PerPage = pageResult.PerPage;
    }

    public List<Notification> Notifications { get; set; }
    public long Total { get; set; }
    public int Page { get; set; }
    public int PerPage { get; set; }
}
