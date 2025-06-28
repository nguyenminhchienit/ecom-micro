using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Contract.Common.Interfaces;
using EventBus.Messages.IntegrationEvents.Interfaces;
using Infrastructure.Common;
using Infrastructure.Extensions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Configurations;

namespace Basket.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var eventSetting = configuration.GetSection(nameof(EventBusSettings)).Get<EventBusSettings>();
            services.AddSingleton(eventSetting);

            var redisSetting = configuration.GetSection(nameof(CacheSettings)).Get<CacheSettings>();
            services.AddSingleton(redisSetting);
            return services;
        }
        public static IServiceCollection ConfigureServices(this IServiceCollection services) =>
            services.AddScoped<IBasketRepository, BasketRepository>()
                .AddTransient<ISerializerService, SerializerService>()
            ;

        public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisSetting = services.GetOptions<CacheSettings>("CacheSettings");
            if (redisSetting == null || string.IsNullOrEmpty(redisSetting.ConnectionString))
                throw new ArgumentNullException("Redis Connection string is not configured.");

            //Redis Configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisSetting.ConnectionString;
            });
        }

        public static void ConfigureMasstransitRabbitMq(this IServiceCollection services)
        {
            var eventSetting = services.GetOptions<EventBusSettings>("EventBusSettings");
            if (eventSetting == null || string.IsNullOrEmpty(eventSetting.HostAddress))
                throw new ArgumentNullException("EventBusSettings Connection string is not configured.");

            var mqConnection = new Uri(eventSetting.HostAddress);
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(mqConnection);
                });

                //Publish submit order message
                config.AddRequestClient<IBasketCheckoutEvent>();
            });
        }
    }
}
