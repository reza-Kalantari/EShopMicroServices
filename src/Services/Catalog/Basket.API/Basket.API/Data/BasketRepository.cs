using Basket.API.Exceptions;
using Basket.API.Models;
using Marten;

namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(username, cancellationToken);
            if(basket is null)
            {
                throw new BasketNotFoundException(username);
            }
            return basket;

        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
             session.Store(basket);
             await session.SaveChangesAsync();
             return basket;

        }

        public async Task<bool> DeletBasket(string username, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(username, cancellationToken);
            if (basket is null)
            {
                throw new BasketNotFoundException(username);
            }
            session.Delete(basket);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
