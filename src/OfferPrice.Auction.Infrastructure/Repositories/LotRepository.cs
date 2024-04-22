﻿using MassTransit;
using MongoDB.Driver;
using OfferPrice.Auction.Domain.Enums;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Auction.Domain.Models;
using OfferPrice.Common;

namespace OfferPrice.Auction.Infrastructure.Repositories;

public class LotRepository : ILotRepository
{
    private readonly IMongoCollection<Lot> _lots;

    public LotRepository(IMongoDatabase database)
    {
        _lots = database.GetCollection<Lot>("lots");
        _lots.Indexes.CreateMany(new[]
        {
            new CreateIndexModel<Lot>(
                Builders<Lot>.IndexKeys
                    .Text(x=>x.Product.Name)
                    .Ascending(l => l.Product.Id),
                new CreateIndexOptions { Unique = true }
            ),
            new CreateIndexModel<Lot>(
                Builders<Lot>.IndexKeys.Ascending(l => l.Start).Ascending(l => l.Status)
            ),
            new CreateIndexModel<Lot>(
                Builders<Lot>.IndexKeys.Ascending(l => l.Updated).Ascending(l => l.Status)
            )
        });
    }

    public Task Create(Lot lot, CancellationToken token)
    {
        var utc = DateTime.UtcNow;
        lot.Version = 1;
        lot.Created = utc;
        lot.Updated = utc;

        return _lots.InsertOneAsync(lot, cancellationToken: token);
    }

    public Task Delete(string id, string userId, CancellationToken token)
    {
        return _lots.DeleteOneAsync(
            Builders<Lot>.Filter.Eq(x => x.Product.User.Id, userId) &
            Builders<Lot>.Filter.Eq(x => x.Id, id),
            cancellationToken: token);
    }

    public Task<Lot> Get(string id, CancellationToken token)
    {
        return _lots.Find(Builders<Lot>.Filter.Eq(x => x.Id, id)).SingleOrDefaultAsync(token);
    }

    public async Task<PageResult<Lot>> Find<T, K>(
        IQueryFilterBuilder<T> filterBuilder,
        IQuerySortBuilder<K> sortBuilder,
        Paging paging,
        CancellationToken token)
    {
        var lotFilters = filterBuilder as IQueryFilterBuilder<FilterDefinition<Lot>>;
        var lotSorts = sortBuilder as IQuerySortBuilder<SortDefinition<Lot>>;

        var filters = lotFilters.GetFilters();
        var sort = lotSorts.GetSorts();

        var totalTask = _lots.CountDocumentsAsync(filters, cancellationToken: token);
        var lotsTask = _lots.Find(filters)
            .Sort(sort)
            .Skip((paging.Page - 1) * paging.PerPage)
            .Limit(paging.PerPage)
            .ToListAsync(token);

        await Task.WhenAll(totalTask, lotsTask);
        return new PageResult<Lot>(
            page: paging.Page,
            perPage: paging.PerPage,
            total: totalTask.Result,
            items: lotsTask.Result
        );
    }

    public Task<List<Lot>> GetNonStarted(DateTime until, CancellationToken cancellationToken)
    {
        var untilFilter = Builders<Lot>.Filter.Lte(l => l.Start, until);
        var statusFilter = Builders<Lot>.Filter.Eq(l => l.Status, LotStatus.Planned);

        var sort = Builders<Lot>.Sort.Ascending(s => s.Start);

        return _lots
            .Find(untilFilter & statusFilter)
            .Sort(sort)
            .ToListAsync(cancellationToken);
    }

    public Task<List<Lot>> GetNonFinished(DateTime until, CancellationToken cancellationToken)
    {
        var fromFilter = Builders<Lot>.Filter.Lt(l => l.Updated, until);
        var statusFilter = Builders<Lot>.Filter.Eq(l => l.Status, LotStatus.Started);

        return _lots.Find(fromFilter & statusFilter).ToListAsync(cancellationToken);
    }

    public Task Update(Lot lot, string userId, CancellationToken token)
    {
        var version = lot.Version;

        lot.Version++;
        lot.Updated = DateTime.UtcNow;

        var idFilter = Builders<Lot>.Filter.Eq(x => x.Id, lot.Id);
        var versionFilter = Builders<Lot>.Filter.Eq(x => x.Version, version);
        var userIdFilter = Builders<Lot>.Filter.Eq(x => x.Product.User.Id, userId);

        var filters = idFilter & versionFilter & userIdFilter;

        return Update(filters, lot, token);
    }

    public Task Update(Lot lot, CancellationToken token)
    {
        var version = lot.Version;

        lot.Version++;
        lot.Updated = DateTime.UtcNow;

        var idFilter = Builders<Lot>.Filter.Eq(x => x.Id, lot.Id);
        var versionFilter = Builders<Lot>.Filter.Eq(x => x.Version, version);

        var filters = idFilter & versionFilter;

        return Update(filters, lot, token);
    }

    private Task Update(FilterDefinition<Lot> filters, Lot lot, CancellationToken cancellationToken)
    {
        return _lots.UpdateOneAsync(
            filters,
            Builders<Lot>.Update
                .Set(x => x.Product, lot.Product)
                .Set(x => x.Winner, lot.Winner)
                .Set(x => x.CurrentPrice, lot.CurrentPrice)
                .Set(x => x.BetHistory, lot.BetHistory)
                .Set(x => x.Start, lot.Start)
                .Set(x => x.End, lot.End)
                .Set(x => x.Status, lot.Status)
                .Set(x => x.Version, lot.Version)
                .Set(x => x.Updated, lot.Updated),
            cancellationToken: cancellationToken
        );
    }

    public async Task<IEnumerable<Lot>> GetNonPlanned(CancellationToken cancellationToken)
    {
        var sort = Builders<Lot>.Sort.Descending(l => l.Updated);
        var statusFilter = Builders<Lot>.Filter.Eq(l => l.Status, LotStatus.Created);

        return await _lots
            .Find(statusFilter)
            .Sort(sort)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Lot>> GetSameLots(Lot lot, CancellationToken cancellationToken)
    {
        var sort = Builders<Lot>.Sort.Descending(l => l.Start);
        var statusFilter = Builders<Lot>.Filter.Eq(l => l.Status, LotStatus.Planned);

        var categoryFilter = Builders<Lot>.Filter.Eq(l => l.Product.Category, lot.Product.Category);

        return await _lots
            .Find(statusFilter & categoryFilter)
            .Sort(sort)
            .Limit(10)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Lot>> GetFavorities(IEnumerable<string> lotIds, CancellationToken cancellationToken)
    {
        return await _lots
            .Find(Builders<Lot>.Filter.In(x => x.Id, lotIds))
            .ToListAsync(cancellationToken);
    }
}