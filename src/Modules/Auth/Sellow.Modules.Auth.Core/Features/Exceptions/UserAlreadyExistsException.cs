using System.Net;
using Sellow.Modules.Shared.Abstractions.Exceptions;

namespace Sellow.Modules.Auth.Core.Features.Exceptions;

internal sealed class UserAlreadyExistsException : SellowException
{
    public UserAlreadyExistsException() : base("User already exists.")
    {
        StatusCode = HttpStatusCode.Conflict;
        ErrorCode = "user_already_exists";
    }
}