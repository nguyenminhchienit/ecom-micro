﻿namespace Basket.API.Extensions
{
    public static class ServiceExtensions
    {
        /*public static IServiceCollection ConfigureServices(this IServiceCollection services) =>
            services.AddScoped<IBasketRepository, BasketRepository>()
                .AddTransient<ISerializeService, SerializeService>()
            ;*/

        public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetSection("CacheSettings:ConnectionString").Value;
            if (string.IsNullOrEmpty(redisConnectionString))
                throw new ArgumentNullException("Redis Connection string is not configured.");

            //Redis Configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });
        }
    }
}
