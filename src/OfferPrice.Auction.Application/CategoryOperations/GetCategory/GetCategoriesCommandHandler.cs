using AutoMapper;
using MediatR;
using OfferPrice.Auction.Application.Models;
using OfferPrice.Auction.Domain.Interfaces;

namespace OfferPrice.Auction.Application.CategoryOperations.GetCategory;

public class GetCategoriesCommandHandler : IRequestHandler<GetCategoriesCommand, IEnumerable<Category>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoriesCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Category>> Handle(GetCategoriesCommand request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAll(cancellationToken);

        return _mapper.Map<IEnumerable<Category>>(categories);
    }
}
