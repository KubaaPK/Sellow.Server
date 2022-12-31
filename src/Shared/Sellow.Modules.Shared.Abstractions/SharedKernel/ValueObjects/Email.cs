using System.Text.RegularExpressions;
using Sellow.Modules.Shared.Abstractions.SharedKernel.ValueObjects.Exceptions;

namespace Sellow.Modules.Shared.Abstractions.SharedKernel.ValueObjects;

public sealed record Email
{
    private static readonly Regex EmailRegex = new(
        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        RegexOptions.Compiled);

    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEmailException(value);
        }

        value = value.ToLowerInvariant();

        if (!EmailRegex.IsMatch(value))
        {
            throw new InvalidEmailException(value);
        }

        Value = value;
    }

    public static implicit operator Email(string email) => new(email);

    public static implicit operator string(Email email) => email.Value;
}