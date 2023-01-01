using FirebaseAdmin.Auth;
using Microsoft.Extensions.Logging;

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
}