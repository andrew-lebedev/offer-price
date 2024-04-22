using MongoDB.Driver;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Auction.Domain.Models;
using OfferPrice.Auction.Domain.Queries;

namespace OfferPrice.Auction.Infrastructure.Builders;

public class QueryFilterBuilder : IQueryFilterBuilder<FilterDefinition<Lot>>
{
    private FindLotsQuery _findLotsQuery;

    private ICollection<FilterDefinition<Lot>> _filters;

    public QueryFilterBuilder(FindLotsQuery findLotsQuery)
    {
        _findLotsQuery = findLotsQuery;

        _filters = new List<FilterDefinition<Lot>>();
    }

    public IQueryFilterBuilder<FilterDefinition<Lot>> AddCategory()
    {
        var filter = string.IsNullOrWhiteSpace(_findLotsQuery.Category)
            ? Builders<Lot>.Filter.Empty
            : Builders<Lot>.Filter.Eq(l => l.Product.Category, _findLotsQuery.Category);

        _filters.Add(filter);

        return this;
    }

    public IQueryFilterBuilder<FilterDefinition<Lot>> AddLocation()
    {
        var filter = string.IsNullOrWhiteSpace(_findLotsQuery.Location)
            ? Builders<Lot>.Filter.Empty
            : Builders<Lot>.Filter.Eq(l => l.Product.Location, _findLotsQuery.Location);

        _filters.Add(filter);

        return this;
    }

    public IQueryFilterBuilder<FilterDefinition<Lot>> AddMaxPrice()
    {
        var filter = _findLotsQuery.MaxPrice.HasValue
            ? Builders<Lot>.Filter.Lte(l => l.Product.Price, _findLotsQuery.MaxPrice)
            : Builders<Lot>.Filter.Empty;

        _filters.Add(filter);

        return this;
    }

    public IQueryFilterBuilder<FilterDefinition<Lot>> AddMinPrice()
    {
        var filter = _findLotsQuery.MinPrice.HasValue
            ? Builders<Lot>.Filter.Gte(l => l.Product.Price, _findLotsQuery.MinPrice)
            : Builders<Lot>.Filter.Empty;

        _filters.Add(filter);

        return this;
    }

    public IQueryFilterBuilder<FilterDefinition<Lot>> AddOnlyPhotos()
    {
        var filter = _findLotsQuery.OnlyWithPhotos
            ? Builders<Lot>.Filter.Gt(l => l.Product.Images.Count, 0)
            : Builders<Lot>.Filter.Empty;

        _filters.Add(filter);

        return this;
    }

    public IQueryFilterBuilder<FilterDefinition<Lot>> AddOnlyVideos()
    {
        throw new NotImplementedException();
    }

    public IQueryFilterBuilder<FilterDefinition<Lot>> AddProductOwner()
    {
        var filter = string.IsNullOrWhiteSpace(_findLotsQuery.ProductOwnerId)
            ? Builders<Lot>.Filter.Empty
            : Builders<Lot>.Filter.Eq(l => l.Product.User.Id, _findLotsQuery.ProductOwnerId);

        _filters.Add(filter);

        return this;
    }

    public IQueryFilterBuilder<FilterDefinition<Lot>> AddProductStatus()
    {
        var filter = _findLotsQuery.ProductStatus.HasValue
            ? Builders<Lot>.Filter.Eq(l => l.Product.State, _findLotsQuery.ProductStatus.Value)
            : Builders<Lot>.Filter.Empty;

        _filters.Add(filter);

        return this;
    }

    public IQueryFilterBuilder<FilterDefinition<Lot>> AddLotStatus()
    {
        var filter = _findLotsQuery.LotStatus.HasValue
            ? Builders<Lot>.Filter.Eq(l => l.Status, _findLotsQuery.LotStatus.Value)
            : Builders<Lot>.Filter.Empty;

        _filters.Add(filter);

        return this;
    }

    public IQueryFilterBuilder<FilterDefinition<Lot>> AddWinner()
    {
        var filter = string.IsNullOrWhiteSpace(_findLotsQuery.WinnerId)
            ? Builders<Lot>.Filter.Empty
            : Builders<Lot>.Filter.Eq(l => l.Winner.Id, _findLotsQuery.WinnerId);

        _filters.Add(filter);

        return this;
    }

    public FilterDefinition<Lot> GetFilters()
    {
        return Builders<Lot>.Filter.And(_filters);
    }

    public IQueryFilterBuilder<FilterDefinition<Lot>> AddSearching()
    {
        var filter = string.IsNullOrWhiteSpace(_findLotsQuery.Searching)
            ? Builders<Lot>.Filter.Empty
            : Builders<Lot>.Filter.Text(_findLotsQuery.Searching);

        _filters.Add(filter);

        return this;
    }

    public IQueryFilterBuilder<FilterDefinition<Lot>> AddUserBets()
    {
        var filter = string.IsNullOrWhiteSpace(_findLotsQuery.BetsWithUserId)
            ? Builders<Lot>.Filter.Empty
            : Builders<Lot>.Filter.ElemMatch(x => x.BetHistory, y => y.User.Id == _findLotsQuery.BetsWithUserId);

        _filters.Add(filter);

        return this;
    }
}
