using Microsoft.EntityFrameworkCore;
using Sellow.Modules.Auth.Core.Entities;
using Sellow.Modules.Auth.Core.Repositories;

namespace Sellow.Modules.Auth.Core.DAL.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly AuthDbContext _context;

    public UserRepository(AuthDbContext context)
    {
        _context = context;
    }

    public async Task Add(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsUnique(User user, CancellationToken cancellationToken = default) =>
        await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email || x.Username == user.Username,
            cancellationToken) is null;
}