using MediatR;

namespace OfferPrice.Auction.Application.LocationOperations.CreateLocation;

public class CreateLocationCommand : IRequest
{
    public string City { get; set; }

    public IEnumerable<string> Disctricts { get; set; }
}
