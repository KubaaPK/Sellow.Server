using System.Net;
using Sellow.Modules.Shared.Abstractions.Exceptions;

namespace Sellow.Modules.Shared.Abstractions.SharedKernel.ValueObjects.Exceptions;

public sealed class InvalidUsernameException : SellowException
{
    public InvalidUsernameException(string username) : base($"Username '{username}' is invalid.")
    {
        StatusCode = HttpStatusCode.BadRequest;
        ErrorCode = "invalid_username";
    }
}