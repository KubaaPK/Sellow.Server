using System.Net;
using Sellow.Modules.Shared.Abstractions.Exceptions;

namespace Sellow.Modules.Shared.Abstractions.SharedKernel.ValueObjects.Exceptions;

public sealed class InvalidEmailException : SellowException
{
    public InvalidEmailException(string email) : base($"Email '{email}' is invalid.")
    {
        StatusCode = HttpStatusCode.BadRequest;
        ErrorCode = "invalid_username";
    }
}