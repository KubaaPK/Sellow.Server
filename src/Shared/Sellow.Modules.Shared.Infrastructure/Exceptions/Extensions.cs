using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Sellow.Modules.Shared.Infrastructure.Exceptions;

internal static class Extensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services) => services
        .AddScoped<ExceptionHandlerMiddleware>();

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app) => app
        .UseMiddleware<ExceptionHandlerMiddleware>();
}