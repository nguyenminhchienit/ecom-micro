using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Contract.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheService;
        private readonly ISerializerService _serializerService;
        private readonly ILogger _logger;

        public BasketRepository(IDistributedCache redisCacheService, ISerializerService serializerService, ILogger logger)
        {
            _redisCacheService = redisCacheService;
            _serializerService = serializerService;
            _logger = logger;
        }

        public async Task<Cart?> GetBasketByUserName(string username)
        {
            _logger.Information($"BEGIN: GetBasketByUserName {username}");
            var basket = await _redisCacheService.GetStringAsync(username);
            _logger.Information($"END: GetBasketByUserName {username} : RESULT: {basket}");

            return string.IsNullOrEmpty(basket) ? null :
                _serializerService.Deserialize<Cart>(basket);
        }

        public async Task<Cart> UpdateBasket(Cart basket, DistributedCacheEntryOptions options)
        {
            _logger.Information($"BEGIN: UpdateBasket for {basket.UserName}");

            if (options != null)
                await _redisCacheService.SetStringAsync(basket.UserName,
                    _serializerService.Serialize(basket), options);
            else
                await _redisCacheService.SetStringAsync(basket.UserName,
                _serializerService.Serialize(basket));
            var result = await GetBasketByUserName(basket.UserName);

            _logger.Information($"END: UpdateBasket for {basket.UserName} RESULT: {result}");

            return result;
        }

        public async Task<bool> DeleteBasketFromUserName(string username)
        {
            try
            {
                _logger.Information($"BEGIN: DeleteBasketFromUserName {username}");
                await _redisCacheService.RemoveAsync(username);
                _logger.Information($"END: DeleteBasketFromUserName {username}");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error("Error DeleteBasketFromUserName: " + e.Message);
                throw;
            }
        }
    }
}
