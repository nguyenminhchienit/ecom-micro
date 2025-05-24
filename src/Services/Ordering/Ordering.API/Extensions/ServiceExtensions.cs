

using Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;

namespace Ordering.API.Extensions
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection(nameof(SMTPEmailSetting))
            .Get<SMTPEmailSetting>();
            services.AddSingleton(emailSettings);
            return services;
        }
            
    }
}
