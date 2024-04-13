using AutoMapper;
using MediatR;
using OfferPrice.Auction.Application.Exceptions;
using OfferPrice.Auction.Application.Models;
using OfferPrice.Auction.Domain.Interfaces;

namespace OfferPrice.Auction.Application.LikeOperations.GetLike;

public class GetLikeCommandHandler :
    IRequestHandler<GetLikeCommand, Like>,
    IRequestHandler<GetCountLikeCommand, long>
{
    private readonly ILikeRepository _likeRepository;
    private readonly IMapper _mapper;

    public GetLikeCommandHandler(ILikeRepository likeRepository, IMapper mapper)
    {
        _likeRepository = likeRepository;
        _mapper = mapper;
    }

    public async Task<Like> Handle(GetLikeCommand request, CancellationToken cancellationToken)
    {
        var like = await _likeRepository.Get(request.LotId, request.UserId, cancellationToken);

        if (like == null)
        {
            throw new EntityNotFoundException("Like is not found");
        }

        return _mapper.Map<Like>(like);
    }

    public Task<long> Handle(GetCountLikeCommand request, CancellationToken cancellationToken)
    {
        return _likeRepository.GetCount(request.LotId, cancellationToken);
    }
}
