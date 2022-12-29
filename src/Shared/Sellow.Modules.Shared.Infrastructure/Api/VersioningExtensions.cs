using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Sellow.Modules.Shared.Infrastructure.Api;

internal static class VersioningExtensions
{
    public static IServiceCollection AddVersioning(this IServiceCollection services) => services
        .AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });
}