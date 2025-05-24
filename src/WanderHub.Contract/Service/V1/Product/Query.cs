using WanderHub.Contract.Abstractions.Message;
using static WanderHub.Contract.Service.V1.Product.Response;

namespace WanderHub.Contract.Service.V1.Product;
public static class Query
{
   // public record GetProductsQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, IDictionary<string, SortOrder>? SortColumnAndOrder, int PageIndex, int PageSize) : IQuery<PagedResult<ProductResponse>>;
    public record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;
}
