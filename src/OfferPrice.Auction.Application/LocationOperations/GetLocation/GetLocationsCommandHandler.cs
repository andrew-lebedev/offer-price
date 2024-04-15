using AutoMapper;
using MediatR;
using OfferPrice.Auction.Application.Models;
using OfferPrice.Auction.Domain.Interfaces;

namespace OfferPrice.Auction.Application.LocationOperations.GetLocation;

public class GetLocationsCommandHandler : IRequestHandler<GetLocationsCommand, IEnumerable<Location>>
{
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public GetLocationsCommandHandler(ILocationRepository locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Location>> Handle(GetLocationsCommand request, CancellationToken cancellationToken)
    {
        var result = await _locationRepository.GetAll(cancellationToken);

        return _mapper.Map<IEnumerable<Location>>(result);
    }
}
