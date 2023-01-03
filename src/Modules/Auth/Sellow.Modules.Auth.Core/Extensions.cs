using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sellow.Modules.Auth.Core.Auth;
using Sellow.Modules.Auth.Core.DAL;

namespace Sellow.Modules.Auth.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services) => services
        .AddDal()
        .AddMediatR(Assembly.GetExecutingAssembly())
        .AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true)
        .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
        .AddAuth();

    public static IApplicationBuilder UseCore(this IApplicationBuilder app) => app
        .UseAuth();
}