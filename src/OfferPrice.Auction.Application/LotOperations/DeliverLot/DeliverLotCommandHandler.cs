using AutoMapper;
using MediatR;
using OfferPrice.Auction.Application.Exceptions;
using OfferPrice.Auction.Application.Models;
using OfferPrice.Auction.Domain.Interfaces;

namespace OfferPrice.Auction.Application.LotOperations.DeliverLot;

public class DeliverLotCommandHandler : IRequestHandler<DeliverLotCommand, Lot>
{
    private readonly ILotRepository _lotRepository;
    private readonly IMapper _mapper;

    public DeliverLotCommandHandler(ILotRepository lotRepository, IMapper mapper)
    {
        _lotRepository = lotRepository;
        _mapper = mapper;
    }

    public async Task<Lot> Handle(DeliverLotCommand request, CancellationToken cancellationToken)
    {
        var lot = await _lotRepository.Get(request.Id, cancellationToken);

        if (lot == null)
        {
            throw new EntityNotFoundException("Lot is not found");
        }

        if (!lot.IsSold())
        {
            throw new LotException("Lot have not sold yet");
        }

        lot.Deliver();
        await _lotRepository.Update(lot, request.UserId, cancellationToken);

        return _mapper.Map<Lot>(lot);
    }
}
