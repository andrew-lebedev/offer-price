using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Common;
using OfferPrice.Notifications.Api.Models;
using OfferPrice.Notifications.Application.Models;
using OfferPrice.Notifications.Application.NotificationOperations.GetNotification;
using OfferPrice.Notifications.Application.NotificationOperations.ReadNotification;
using OfferPrice.Notifications.Application.NotificationSettingsOperations.GetSettings;
using OfferPrice.Notifications.Application.NotificationSettingsOperations.SwitchSettings;

namespace OfferPrice.Notifications.Api.Controllers;

[Route("api/v{version:apiVersion}/notifications")]
[ApiController]
[ApiVersion("1.0")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public NotificationController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Paging paging, CancellationToken cancellationToken)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var cmd = new GetNotificationsCommand() { UserId = userId, Paging = paging };

        var result = await _mediator.Send(cmd, cancellationToken);

        return Ok(new NotificationResponse(result));
    }

    [HttpPost("read")]
    public async Task<IActionResult> Read([FromBody] ICollection<Notification> notifications, CancellationToken cancellationToken)
    {
        var cmd = new ReadNotificationCommand() { Notifications = notifications };

        await _mediator.Send(cmd, cancellationToken);

        return Ok();
    }

    [HttpPost("settings")]
    public async Task<IActionResult> SwitchSettings([FromBody] bool switchSettings, CancellationToken cancellationToken)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var cmd = new SwitchSettingsCommand() { UserId = userId, SwitchSettings = switchSettings };

        await _mediator.Send(cmd, cancellationToken);

        return Ok();
    }

    [HttpGet("settings")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var cmd = new GetSettingsCommand() { UserId = userId };

        var response = await _mediator.Send(cmd, cancellationToken);

        return Ok(response);
    }
}
