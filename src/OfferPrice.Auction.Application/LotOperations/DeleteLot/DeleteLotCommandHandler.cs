using MediatR;
using OfferPrice.Auction.Domain.Interfaces;

namespace OfferPrice.Auction.Application.LotOperations.DeleteLot;

public class DeleteLotCommandHandler : IRequestHandler<DeleteLotCommand>
{
    private readonly ILotRepository _lotRepository;

    public DeleteLotCommandHandler(
        ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    public Task Handle(DeleteLotCommand request, CancellationToken cancellationToken)
    {
        return _lotRepository.Delete(request.Id, request.UserId, cancellationToken);
    }
}
