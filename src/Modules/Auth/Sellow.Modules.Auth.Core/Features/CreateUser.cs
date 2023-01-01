using MediatR;
using Microsoft.Extensions.Logging;
using Sellow.Modules.Auth.Core.Entities;
using Sellow.Modules.Auth.Core.Features.Exceptions;
using Sellow.Modules.Auth.Core.Repositories;
using Sellow.Modules.Auth.IntegrationEvents;

namespace Sellow.Modules.Auth.Core.Features;

internal sealed record CreateUser(string Email, string Username, string Password) : IRequest<Guid>;

internal sealed class CreateUserHandler : IRequestHandler<CreateUser, Guid>
{
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(ILogger<CreateUserHandler> logger, IMediator mediator, IUserRepository userRepository)
    {
        _logger = logger;
        _mediator = mediator;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var user = new User(request.Email, request.Username);

        var isUserUnique = await _userRepository.IsUnique(user, cancellationToken);

        if (isUserUnique is false)
        {
            throw new UserAlreadyExistsException();
        }

        await _userRepository.Add(user, cancellationToken);

        _logger.LogInformation("User '{Id}' has been added to database", user.Id);

        await _mediator.Publish(new UserCreated(user.Id, user.Email, user.Username), cancellationToken);

        return user.Id;
    }
}