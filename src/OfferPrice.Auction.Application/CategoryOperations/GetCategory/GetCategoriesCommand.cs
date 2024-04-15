using MediatR;
using OfferPrice.Auction.Application.Models;

namespace OfferPrice.Auction.Application.CategoryOperations.GetCategory;

public class GetCategoriesCommand : IRequest<IEnumerable<Category>>
{
}
