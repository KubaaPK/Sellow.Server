using System.Security.Claims;

namespace Sellow.Modules.Shared.Infrastructure.Auth;

public static class Extensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal) =>
        Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
}