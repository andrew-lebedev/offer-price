using MediatR;
using OfferPrice.Auction.Domain.Interfaces;

namespace OfferPrice.Auction.Application.LikeOperations.DeleteLike;

public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand>
{
    private readonly ILikeRepository _likeRepository;

    public DeleteLikeCommandHandler(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public Task Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
    {
        return _likeRepository.Delete(request.LotId, request.UserId, cancellationToken);
    }
}
