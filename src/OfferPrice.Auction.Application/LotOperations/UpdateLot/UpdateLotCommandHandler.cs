using AutoMapper;
using MediatR;
using OfferPrice.Auction.Domain.Interfaces;

namespace OfferPrice.Auction.Application.LotOperations.UpdateLot;

public class UpdateLotCommandHandler : IRequestHandler<UpdateLotCommand>
{
    private readonly ILotRepository _lotRepository;
    private readonly IMapper _mapper;

    public UpdateLotCommandHandler(ILotRepository lotRepository, IMapper mapper)
    {
        _lotRepository = lotRepository;
        _mapper = mapper;
    }

    public Task Handle(UpdateLotCommand request, CancellationToken cancellationToken)
    {
        var lot = _mapper.Map<Domain.Models.Lot>(request);

        return _lotRepository.Update(lot, request.UserId, cancellationToken);
    }
}
