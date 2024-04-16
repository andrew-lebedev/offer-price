using MediatR;

namespace OfferPrice.Auction.Application.CategoryOperations.CreateCategory;

public class CreateCategoryCommand : IRequest
{
    public string Name { get; set; }

}
