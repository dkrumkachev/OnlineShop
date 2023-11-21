using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OnlineShop.Services.Basket.DataLayer.Models.Data;
using OnlineShop.Services.Basket.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Basket.DataLayer.Repositories.Implementations
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<ShoppingCart?> GetBasketAsync(string userId, CancellationToken cancellationToken = default)
        {
            var basket = await _redisCache.GetStringAsync(userId, cancellationToken);

            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await _redisCache.SetStringAsync(basket.UserId, JsonConvert.SerializeObject(basket), cancellationToken);

            return await GetBasketAsync(basket.UserId, cancellationToken);
        }

        public async Task DeleteBasketAsync(string userId, CancellationToken cancellationToken = default)
        {
            await _redisCache.RemoveAsync(userId, cancellationToken);
        }
    }
}
