using Microsoft.Extensions.DependencyInjection;
using Sellow.Modules.Auth.Core.DAL.Repositories;
using Sellow.Modules.Auth.Core.Repositories;
using Sellow.Modules.Shared.Infrastructure.DAL.Postgres;

namespace Sellow.Modules.Auth.Core.DAL;

internal static class Extensions
{
    public static IServiceCollection AddDal(this IServiceCollection services) => services
        .AddPostgres<AuthDbContext>()
        .AddScoped<IUserRepository, UserRepository>();
}