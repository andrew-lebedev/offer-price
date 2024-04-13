using MediatR;
using OfferPrice.Auction.Domain.Interfaces;

namespace OfferPrice.Auction.Application.LikeOperations.CreateLike;

internal class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand>
{
    private readonly ILikeRepository _likeRepository;

    public CreateLikeCommandHandler(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public Task Handle(CreateLikeCommand request, CancellationToken cancellationToken)
    {
        var like = new Domain.Models.Like
        {
            LotId = request.LotId,
            UserId = request.UserId,
        };

        return _likeRepository.Create(like, cancellationToken);
    }
}
