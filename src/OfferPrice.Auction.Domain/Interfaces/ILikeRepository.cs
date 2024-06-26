﻿using System.Threading.Tasks;
using System.Threading;
using OfferPrice.Auction.Domain.Models;

namespace OfferPrice.Auction.Domain.Interfaces;

public interface ILikeRepository
{
    Task<Like> Get(string lotId, string userId, CancellationToken token);

    Task<long> GetCount(string lotId, CancellationToken token);

    Task Create(Like like, CancellationToken token);

    Task Delete(string lotId, string userId, CancellationToken token);
}
