namespace Catalog.Api.Contracts
{
    public record PaginatedData<T>(IEnumerable<T> Data, int PageSize, int PageIndex, int Total);
}