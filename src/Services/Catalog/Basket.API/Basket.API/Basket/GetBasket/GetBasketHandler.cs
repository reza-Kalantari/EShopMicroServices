using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using Mapster;

namespace Basket.API.Basket.GetBasket
{
    public  record GetBasketQuery(string Username): IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);
    public class GetBasketQueryHandler (IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var shoppingCart = await repository.GetBasket(query.Username);
            return new GetBasketResult(shoppingCart);
        }
    }
}
