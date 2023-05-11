namespace OfferPrice.Auction.Domain;
public interface IAuctionRepository
{
    Task<List<Auction>> Get(CancellationToken token);

    Task<Auction> GetById(string id, CancellationToken token);

    Task Create(Auction auction, CancellationToken token);

    Task Update(Auction auction, CancellationToken token);

    Task Delete(string id, CancellationToken token);
}

