using MediatR;
using OfferPrice.Auction.Application.Exceptions;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Auction.Domain.Models;

namespace OfferPrice.Auction.Application.LocationOperations.CreateLocation;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand>
{
    private readonly ILocationRepository _locationRepository;

    public CreateLocationCommandHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.GetByCity(request.City, cancellationToken);

        if (location is not null)
        {
            throw new ConflictException("This location already exists");
        }

        location = new Location()
        {
            Id = Guid.NewGuid().ToString(),
            City = request.City,
            Districts = request.Disctricts
        };

        await _locationRepository.Create(location, cancellationToken);
    }
}
