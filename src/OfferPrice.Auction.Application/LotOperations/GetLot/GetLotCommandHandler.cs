using AutoMapper;
using MediatR;
using OfferPrice.Auction.Application.Models;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Common;

namespace OfferPrice.Auction.Application.LotOperations.GetLot;

public class GetLotCommandHandler : 
    IRequestHandler<GetLotCommand, Lot>,
    IRequestHandler<GetLotsCommand, PageResult<Lot>>
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

    public async Task<PageResult<Lot>> Handle(GetLotsCommand request, CancellationToken cancellationToken)
    {
         var result = await _lotRepository.Find(request.Query, cancellationToken);

        return _mapper.Map<PageResult<Lot>>(result);
    }
}
