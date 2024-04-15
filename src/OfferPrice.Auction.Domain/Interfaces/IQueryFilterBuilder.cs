namespace OfferPrice.Auction.Domain.Interfaces;

public interface IQueryFilterBuilder<T>
{
    IQueryFilterBuilder<T> AddProductOwner();

    IQueryFilterBuilder<T> AddWinner();

    IQueryFilterBuilder<T> AddLotStatus();

    IQueryFilterBuilder<T> AddCategory();

    IQueryFilterBuilder<T> AddLocation();

    IQueryFilterBuilder<T> AddMinPrice();

    IQueryFilterBuilder<T> AddMaxPrice();

    IQueryFilterBuilder<T> AddProductStatus();

    IQueryFilterBuilder<T> AddOnlyPhotos();

    IQueryFilterBuilder<T> AddOnlyVideos();

    T GetFilters();
}
