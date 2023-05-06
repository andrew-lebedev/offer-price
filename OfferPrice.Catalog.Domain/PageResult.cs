namespace OfferPrice.Catalog.Domain;
public class PageResult<T>
{
    public int Page { get; set; }

    public int PerPage { get; set; }

    public long TotalPages { get; set; }

    public List<T> Items { get; set; }

    public PageResult(int page, int perPage, long totalPages, List<T> items)
    {
        Page = page;
        PerPage = perPage;
        TotalPages = totalPages;
        Items = items;
    }
}

