
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsRequest(int? PageNumber, int? PageSize);

    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest getProductRequest, ISender sender) =>
            {
                var query = getProductRequest.Adapt<GetProductsQuery>();
                var result = await sender.Send(query);
                return Results.Ok(result.Adapt<GetProductsResponse>());
            }).WithName("Get Products")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("get Products")
            .WithDescription("Get Products");
        }
    }
}
