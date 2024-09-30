
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductsByCategory
{
    //public record GetProductByCategoryRequest();
    public record GetProductByCategoryResponse(IEnumerable<Product> products);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category,ISender sender) =>
            {
                var products = await sender.Send(new GetProductByCategoryQuery(category));
                var response = products.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            }).WithName("Get Products by category")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("get Products by category")
            .WithDescription("Get Products By Category");
        }
    }
}
