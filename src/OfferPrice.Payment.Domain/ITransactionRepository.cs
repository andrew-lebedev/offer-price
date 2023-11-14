namespace OfferPrice.Payment.Domain;

public interface ITransactionRepository
{
    Task Create(Transaction transaction, CancellationToken token);

    Task<Transaction> GetById(string id, CancellationToken token);
}

