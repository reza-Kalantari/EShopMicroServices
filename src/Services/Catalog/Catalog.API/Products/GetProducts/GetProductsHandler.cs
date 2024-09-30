
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber, int? PageSize) : IQuery<GetProductResult>;
    public  record GetProductResult(IEnumerable<Product> Products);
    public class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        async Task<GetProductResult> IRequestHandler<GetProductsQuery, GetProductResult>.Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            
            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 5, cancellationToken);
            return new GetProductResult(products);
        }
    }
}
