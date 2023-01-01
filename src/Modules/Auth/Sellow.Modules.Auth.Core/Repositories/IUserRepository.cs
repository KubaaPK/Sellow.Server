using Sellow.Modules.Auth.Core.Entities;

namespace Sellow.Modules.Auth.Core.Repositories;

internal interface IUserRepository
{
    Task Add(User user, CancellationToken cancellationToken = default);
    Task<bool> IsUnique(User user, CancellationToken cancellationToken = default);
}