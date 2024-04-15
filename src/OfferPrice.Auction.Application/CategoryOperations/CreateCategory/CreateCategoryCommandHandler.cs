using MediatR;
using OfferPrice.Auction.Domain.Interfaces;

namespace OfferPrice.Auction.Application.CategoryOperations.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByName(request.Name, cancellationToken);

        if (category is not null)
        {
            throw new Exception("This category already exists"); // todo
        }

        category = new Domain.Models.Category { Name = request.Name };

        await _categoryRepository.Create(category, cancellationToken);
    }
}
