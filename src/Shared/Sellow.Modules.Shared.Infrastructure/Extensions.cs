using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sellow.Modules.Shared.Infrastructure.Api;
using Sellow.Modules.Shared.Infrastructure.Exceptions;
using Sellow.Modules.Shared.Infrastructure.Swagger;

namespace Sellow.Modules.Shared.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddControllers().ConfigureApplicationPartManager(manager =>
            manager.FeatureProviders.Add(new InternalControllerFeatureProvider()));

        return services
            .AddVersioning()
            .AddErrorHandling()
            .AddSwagger();
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app) => app
        .UseErrorHandling()
        .UseSwagger_();
}