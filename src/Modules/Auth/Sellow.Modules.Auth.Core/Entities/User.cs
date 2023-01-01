using Sellow.Modules.Shared.Abstractions.SharedKernel.ValueObjects;

namespace Sellow.Modules.Auth.Core.Entities;

internal sealed class User
{
    public Guid Id { get; }
    public Email Email { get; }
    public Username Username { get; }

    public User(Email email, Username username)
    {
        Email = email;
        Username = username;
    }
}