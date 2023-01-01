using FirebaseAdmin.Auth;
using Microsoft.Extensions.Logging;
using Sellow.Modules.Auth.Core.Auth.Firebase.Exceptions;

namespace Sellow.Modules.Auth.Core.Auth.Firebase;

internal sealed class FirebaseAuthService : IAuthService
{
    private readonly ILogger<FirebaseAuthService> _logger;

    public FirebaseAuthService(ILogger<FirebaseAuthService> logger)
    {
        _logger = logger;
    }

    public async Task CreateUser(ExternalAuthUser user)
    {
        await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs
        {
            Uid = user.Id.ToString(),
            Email = user.Email,
            DisplayName = user.Username,
            Password = user.Password,
            Disabled = true,
            EmailVerified = false
        });

        _logger.LogInformation("Firebase user '{Id}' has been created", user.Id);
    }

    public async Task ActivateUser(Guid id)
    {
        try
        {
            var firebaseUser = await FirebaseAuth.DefaultInstance.GetUserAsync(id.ToString());

            if (firebaseUser.EmailVerified)
            {
                throw new FirebaseUserNotFoundOrIsAlreadyActivatedException();
            }

            await FirebaseAuth.DefaultInstance.UpdateUserAsync(new UserRecordArgs
            {
                Uid = id.ToString(),
                Disabled = false,
                EmailVerified = true
            });

            _logger.LogInformation("Firebase user '{Id}' has been activated", id);
        }
        catch (FirebaseAuthException exception)
        {
            if (exception.AuthErrorCode == AuthErrorCode.UserNotFound)
            {
                throw new FirebaseUserNotFoundOrIsAlreadyActivatedException();
            }

            throw;
        }
    }
}