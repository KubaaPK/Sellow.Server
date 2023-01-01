using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sellow.Modules.Auth.Core.DAL;

namespace Sellow.Modules.Auth.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services) => services
        .AddDal()
        .AddMediatR(Assembly.GetExecutingAssembly());

    public static IApplicationBuilder UseCore(this IApplicationBuilder app) => app;
}