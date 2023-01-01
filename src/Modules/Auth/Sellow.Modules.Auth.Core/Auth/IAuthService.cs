namespace Sellow.Modules.Auth.Core.Auth;

internal interface IAuthService
{
    Task CreateUser(ExternalAuthUser user);
    Task ActivateUser(Guid id);
}