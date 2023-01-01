using System.Net;
using Sellow.Modules.Shared.Abstractions.Exceptions;

namespace Sellow.Modules.Auth.Core.Auth.Firebase.Exceptions;

internal sealed class FirebaseUserNotFoundOrIsAlreadyActivatedException : SellowException
{
    public FirebaseUserNotFoundOrIsAlreadyActivatedException() : base("User not found or is already activated.")
    {
        StatusCode = HttpStatusCode.UnprocessableEntity;
        ErrorCode = "user_not_found_or_is_already_activated";
    }
}