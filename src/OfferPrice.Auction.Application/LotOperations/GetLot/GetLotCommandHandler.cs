﻿using AutoMapper;
using MediatR;
using OfferPrice.Auction.Application.Exceptions;
using OfferPrice.Auction.Application.Models;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Auction.Infrastructure.Builders;
using OfferPrice.Auction.Infrastructure.Directors;
using OfferPrice.Common;

namespace OfferPrice.Auction.Application.LotOperations.GetLot;

public class GetLotCommandHandler :
    IRequestHandler<GetLotCommand, Lot>,
    IRequestHandler<GetRegularLotsCommand, PageResult<Lot>>,
    IRequestHandler<GetUserLotsCommand, PageResult<Lot>>,
    IRequestHandler<GetFavoriteLotsCommand, PageResult<Lot>>,
    IRequestHandler<GetLotsWithUserBetsCommand, PageResult<Lot>>
{
    private readonly ILotRepository _lotRepository;
    private readonly ILikeRepository _likeRepository;
    private readonly IMapper _mapper;

    public GetLotCommandHandler(ILotRepository lotRepository, ILikeRepository likeRepository, IMapper mapper)
    {
        _lotRepository = lotRepository;
        _likeRepository = likeRepository;
        _mapper = mapper;
    }

    public async Task<Lot> Handle(GetLotCommand request, CancellationToken cancellationToken)
    {
        var lot = await _lotRepository.Get(request.Id, cancellationToken);

        if (lot == null)
        {
            throw new EntityNotFoundException("Lot is not found");
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

        return new PageResult<Lot>(
            result.Page,
            result.PerPage,
            result.Total, 
            _mapper.Map<List<Lot>>(result.Items));
    }

    public async Task<PageResult<Lot>> Handle(GetUserLotsCommand request, CancellationToken cancellationToken)
    {
        var filterBuilder = new QueryFilterBuilder(request.Query);
        var sortBuidler = new QuerySortBuilder(request.Query);
        var director = new UserLotsDirector(filterBuilder);
        director.Build();

        var result = await _lotRepository.Find(filterBuilder, sortBuidler, request.Query.Paging, cancellationToken);

        return new PageResult<Lot>(
            result.Page, 
            result.PerPage,
            result.Total, 
            _mapper.Map<List<Lot>>(result.Items));
    }

    public async Task<PageResult<Lot>> Handle(GetFavoriteLotsCommand request, CancellationToken cancellationToken)
    {
        var lots = await _likeRepository.Get(request.UserId, request.Paging, cancellationToken);

        var favorities = _lotRepository.GetFavorities(lots.Items, cancellationToken);

        return new PageResult<Lot>(
            lots.Page,
            lots.PerPage,
            lots.Total,
            _mapper.Map<List<Lot>>(favorities));
    }

    public async Task<PageResult<Lot>> Handle(GetLotsWithUserBetsCommand request, CancellationToken cancellationToken)
    {
        var filterBuilder = new QueryFilterBuilder(request.Query);
        var sortBuidler = new QuerySortBuilder(request.Query);

        filterBuilder
            .AddUserBets();

        var result = await _lotRepository.Find(filterBuilder, sortBuidler, request.Query.Paging, cancellationToken);

        return new PageResult<Lot>(
            result.Page,
            result.PerPage,
            result.Total,
            _mapper.Map<List<Lot>>(result.Items));
    }
}
