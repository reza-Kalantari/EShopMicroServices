using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository,IDistributedCache cache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
        {
           var cachedBasket = await cache.GetStringAsync(username, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket)) {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }
           var basket = await repository.GetBasket(username, cancellationToken);
            await cache.SetStringAsync(username, JsonSerializer.Serialize<ShoppingCart>(basket));
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasket(basket, cancellationToken);
            await cache.SetStringAsync(basket.Username, JsonSerializer.Serialize<ShoppingCart>(basket));
            return basket;

        }

        public async Task<bool> DeletBasket(string username, CancellationToken cancellationToken = default)
        {
            await cache.RemoveAsync(username, cancellationToken);
            await repository.DeletBasket(username, cancellationToken);
            return true;
        }
    }
}
