using MongoDB.Driver;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Auction.Domain.Models;

namespace OfferPrice.Auction.Infrastructure.Directors;

public class UserLotsDirector
{
    private readonly IQueryFilterBuilder<FilterDefinition<Lot>> _queryFilterBuilder;
    
    public UserLotsDirector(IQueryFilterBuilder<FilterDefinition<Lot>> queryFilterBuilder)
    {
        _queryFilterBuilder = queryFilterBuilder;
    }

    public void Build()
    {
        _queryFilterBuilder
            .AddProductOwner()
            .AddWinner()
            .AddLotStatus();
    }
}
