using Basket.API.Models;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Basket.API.Basket.GetBasket
{
    //public record GetProductRequest()
    public  record  GetbasketResponse(ShoppingCart Cart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{username}", async (string username, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(username));
                var response = result.Adapt<GetbasketResponse>();
                return Results.Ok(response);
            }).WithName("GetBasketByUsername")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get basket by username")
            .WithDescription("Get basket by username");
        }
    }
}
