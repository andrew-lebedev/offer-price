using MongoDB.Driver;
using OfferPrice.Auction.Domain.Models;
using OfferPrice.Auction.Domain.Interfaces;

namespace OfferPrice.Auction.Infrastructure.Directors;

public class RegularLotsDirector
{
    private readonly IQueryFilterBuilder<FilterDefinition<Lot>> _queryFilterBuilder;

    private readonly IQuerySortBuilder<SortDefinition<Lot>> _querySortBuilder;

    public RegularLotsDirector(
        IQueryFilterBuilder<FilterDefinition<Lot>> queryFilterBuilder,
        IQuerySortBuilder<SortDefinition<Lot>> querySortBuilder)
    {
        _queryFilterBuilder = queryFilterBuilder;
        _querySortBuilder = querySortBuilder;
    }

    public void Build()
    {
        _queryFilterBuilder
            .AddCategory()
            .AddLocation()
            .AddMaxPrice()
            .AddMinPrice()
            .AddOnlyPhotos()
            .AddProductStatus();

        _querySortBuilder
           .AddSorting();
    }
}
