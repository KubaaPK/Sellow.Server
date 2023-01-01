using MediatR;
using Microsoft.Extensions.Logging;
using Sellow.Modules.Auth.Core.Auth;

namespace Sellow.Modules.Auth.Core.Features;

internal sealed record ActivateUser(Guid Id) : IRequest;

internal sealed class ActivateUserHandler : IRequestHandler<ActivateUser>
{
    private readonly ILogger<ActivateUserHandler> _logger;
    private readonly IAuthService _authService;

    public ActivateUserHandler(ILogger<ActivateUserHandler> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    public async Task<Unit> Handle(ActivateUser request, CancellationToken cancellationToken)
    {
        await _authService.ActivateUser(request.Id);

        return Unit.Value;
    }
}