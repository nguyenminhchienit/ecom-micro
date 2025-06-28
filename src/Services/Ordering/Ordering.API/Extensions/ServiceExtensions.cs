using Infrastructure.Configurations;
using Infrastructure.Extensions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ordering.API.Application.IntegrationEvents.EventHandler;
using Shared.Configurations;

namespace Ordering.API.Extensions
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection(nameof(SMTPEmailSetting))
            .Get<SMTPEmailSetting>();
            services.AddSingleton(emailSettings);

            var eventSetting = configuration.GetSection(nameof(EventBusSettings)).Get<EventBusSettings>();
            services.AddSingleton(eventSetting);

            return services;
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
                config.AddConsumersFromNamespaceContaining<BasketCheckoutEventHandler>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(mqConnection);

                    //cfg.ReceiveEndpoint("basket-checkout-queue", c =>
                    //{
                    //    c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
                    //});
                    cfg.ConfigureEndpoints(ctx);
                });

            });
        }

    }
}
