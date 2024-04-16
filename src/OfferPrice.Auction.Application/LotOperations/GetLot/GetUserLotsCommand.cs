﻿using MediatR;
using OfferPrice.Auction.Application.Models;
using OfferPrice.Auction.Domain.Queries;
using OfferPrice.Common;

namespace OfferPrice.Auction.Application.LotOperations.GetLot;

public class GetUserLotsCommand : IRequest<PageResult<Lot>>
{
    public FindLotsQuery Query { get; set; }
}
