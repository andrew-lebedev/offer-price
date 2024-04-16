using MediatR;
using OfferPrice.Auction.Application.Models;

namespace OfferPrice.Auction.Application.LocationOperations.GetLocation;

public class GetLocationsCommand : IRequest<IEnumerable<Location>>
{

}
