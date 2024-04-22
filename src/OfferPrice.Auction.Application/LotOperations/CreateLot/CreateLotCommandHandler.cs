using AutoMapper;
using MediatR;
using OfferPrice.Auction.Application.Exceptions;
using OfferPrice.Auction.Domain.Interfaces;

namespace OfferPrice.Auction.Application.LotOperations.CreateLot;

public class CreateLotCommandHandler : IRequestHandler<CreateLotCommand>
{
    private readonly ILotRepository _lotRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateLotCommandHandler(
        ILotRepository lotRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _lotRepository = lotRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task Handle(CreateLotCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.UserId, cancellationToken);

        if (user == null)
        {
            throw new EntityNotFoundException("User is not found");
        }

        var lot = _mapper.Map<Domain.Models.Lot>(request);

        lot.Product.User = user;

        await _lotRepository.Create(lot, cancellationToken);
    }
}
