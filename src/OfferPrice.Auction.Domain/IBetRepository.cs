using System.Threading.Tasks;

namespace OfferPrice.Auction.Domain;
public interface IBetRepository
{
    Task GetForAuction(string auctionId);

    Task Create(Bet bet);

    Task Update(string id, Bet bet);

    Task Delete(string id);

}

