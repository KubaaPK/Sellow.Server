using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sellow.Modules.EmailSending.Core.EmailClients;

namespace Sellow.Modules.EmailSending.Core;

internal static class EmailSendingModule
{
    public static IServiceCollection AddEmailSendingModule(this IServiceCollection services) => services
        .AddMediatR(Assembly.GetExecutingAssembly())
        .AddScoped<SendgridClient>();
}