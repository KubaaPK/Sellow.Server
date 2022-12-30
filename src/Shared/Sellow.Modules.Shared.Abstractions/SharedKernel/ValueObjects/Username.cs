using Sellow.Modules.Shared.Abstractions.SharedKernel.ValueObjects.Exceptions;

namespace Sellow.Modules.Shared.Abstractions.SharedKernel.ValueObjects;

public sealed record Username
{
    public string Value { get; }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is < 3 or > 20)
        {
            throw new InvalidUsernameException(value);
        }

        Value = value;
    }

    public static implicit operator Username(string username) => new(username);

    public static implicit operator string(Username username) => username.Value;
}