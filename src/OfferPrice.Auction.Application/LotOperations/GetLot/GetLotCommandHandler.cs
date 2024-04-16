using AutoMapper;
using MediatR;
using OfferPrice.Auction.Application.Models;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Auction.Infrastructure.Builders;
using OfferPrice.Auction.Infrastructure.Directors;
using OfferPrice.Common;

namespace OfferPrice.Auction.Application.LotOperations.GetLot;

public class GetLotCommandHandler :
    IRequestHandler<GetLotCommand, Lot>,
    IRequestHandler<GetRegularLotsCommand, PageResult<Lot>>,
    IRequestHandler<GetUserLotsCommand, PageResult<Lot>>
{
    private readonly ILotRepository _lotRepository;
    private readonly IMapper _mapper;

    public GetLotCommandHandler(ILotRepository lotRepository, IMapper mapper)
    {
        _lotRepository = lotRepository;
        _mapper = mapper;
    }

    public async Task<Lot> Handle(GetLotCommand request, CancellationToken cancellationToken)
    {
        var lot = await _lotRepository.Get(request.Id, cancellationToken);

        if (lot == null)
        {
            throw new Exception();
        }

        return _mapper.Map<Lot>(lot);
    }

    public async Task<PageResult<Lot>> Handle(GetRegularLotsCommand request, CancellationToken cancellationToken)
    {
        var filterBuilder = new QueryFilterBuilder(request.Query);
        var sortBuidler = new QuerySortBuilder(request.Query);
        var director = new RegularLotsDirector(filterBuilder, sortBuidler);
        director.Build();

        var result = await _lotRepository.Find(filterBuilder, sortBuidler, request.Query.Paging, cancellationToken);

        return _mapper.Map<PageResult<Lot>>(result);
    }

    public async Task<PageResult<Lot>> Handle(GetUserLotsCommand request, CancellationToken cancellationToken)
    {
        var filterBuilder = new QueryFilterBuilder(request.Query);
        var sortBuidler = new QuerySortBuilder(request.Query);
        var director = new UserLotsDirector(filterBuilder);
        director.Build();

        var result = await _lotRepository.Find(filterBuilder, sortBuidler, request.Query.Paging, cancellationToken);

        return _mapper.Map<PageResult<Lot>>(result);
    }
}
