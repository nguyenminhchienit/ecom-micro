using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Common.Behaviors;
using System.Reflection;

namespace Ordering.Application
{
    public static class ConfigServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services.AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
            ;
    }
}
