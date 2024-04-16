namespace OfferPrice.Auction.Domain.Interfaces;

public interface IQuerySortBuilder<T>
{
    IQuerySortBuilder<T> AddSorting();

    T GetSorts();
}
