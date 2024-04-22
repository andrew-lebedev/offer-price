using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Auction.Api.Models.Requests;
using OfferPrice.Auction.Application.LocationOperations.CreateLocation;
using OfferPrice.Auction.Application.LocationOperations.GetLocation;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/locations")]
public class LocationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public LocationController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var cmd = new GetLocationsCommand();

        var result = await _mediator.Send(cmd, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLocationRequest request, CancellationToken cancellationToken)
    {
        var cmd = _mapper.Map<CreateLocationCommand>(request);

        await _mediator.Send(cmd, cancellationToken);

        return Ok();
    }
}
