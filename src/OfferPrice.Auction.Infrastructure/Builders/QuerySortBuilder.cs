using MongoDB.Driver;
using OfferPrice.Auction.Domain.Enums;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Auction.Domain.Models;
using OfferPrice.Auction.Domain.Queries;

namespace OfferPrice.Auction.Infrastructure.Builders;

public class QuerySortBuilder : IQuerySortBuilder<SortDefinition<Lot>>
{
    private FindLotsQuery _findLotsQuery;

    private ICollection<SortDefinition<Lot>> _sorts;

    public QuerySortBuilder(FindLotsQuery findLotsQuery)
    {
        _findLotsQuery = findLotsQuery;
        _sorts = new List<SortDefinition<Lot>>();
    }

    public IQuerySortBuilder<SortDefinition<Lot>> AddSorting()
    {
        if (_findLotsQuery.SortType.HasValue)
        {
            var sort = _findLotsQuery.SortType.Value switch
            {
                SortType.Ascending => Builders<Lot>.Sort.Ascending(x => x.Product.Price),
                SortType.Descending => Builders<Lot>.Sort.Descending(x => x.Product.Price),
                SortType.Novetly => Builders<Lot>.Sort.Ascending(x => x.Created),
                _ => throw new ArgumentNullException()
            };

            _sorts.Add(sort);
        }

        return this;
    }

    public SortDefinition<Lot> GetSorts()
    {
        return Builders<Lot>.Sort.Combine(_sorts);
    }
}
