using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Sellow.Modules.Shared.Infrastructure.Logging;

internal static class SerilogExtensions
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((_, ctx) => ctx.WriteTo.Console());
        return builder;
    }
}