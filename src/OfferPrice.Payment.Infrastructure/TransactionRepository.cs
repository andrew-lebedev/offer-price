using MongoDB.Driver;
using OfferPrice.Payment.Domain;

namespace OfferPrice.Payment.Infrastructure;

public class TransactionRepository : ITransactionRepository
{
    private readonly IMongoCollection<Transaction> _transaction;

    public TransactionRepository(IMongoDatabase database)
    {
        _transaction = database.GetCollection<Transaction>("transactions");
    }

    public Task Create(Transaction transaction, CancellationToken token)
    {
        return _transaction.InsertOneAsync(transaction, token);
    }

    public Task<Transaction> GetById(string id, CancellationToken token)
    {
        return _transaction.Find(Builders<Transaction>.Filter.Eq(x => x.Id, id))
                     .SingleOrDefaultAsync(token);
    }
}

