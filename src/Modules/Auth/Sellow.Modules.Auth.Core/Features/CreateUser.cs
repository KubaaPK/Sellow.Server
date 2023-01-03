using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Sellow.Modules.Auth.Core.Auth;
using Sellow.Modules.Auth.Core.Entities;
using Sellow.Modules.Auth.Core.Features.Exceptions;
using Sellow.Modules.Auth.Core.Repositories;
using Sellow.Modules.Auth.IntegrationEvents;

namespace Sellow.Modules.Auth.Core.Features;

internal sealed record CreateUser : IRequest<Guid>
{
    [Required] [EmailAddress] public string Email { get; init; }

    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Username { get; init; }

    [Required] [MinLength(6)] public string Password { get; init; }
}

internal class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-mail address is required.")
            .EmailAddress().WithMessage("Invalid e-mail address.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(20).WithMessage("Username can have up to 20 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}

internal sealed class CreateUserHandler : IRequestHandler<CreateUser, Guid>
{
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public CreateUserHandler(ILogger<CreateUserHandler> logger, IMediator mediator, IUserRepository userRepository,
        IAuthService authService)
    {
        _logger = logger;
        _mediator = mediator;
        _userRepository = userRepository;
        _authService = authService;
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

        await _authService.CreateUser(new ExternalAuthUser(user.Id, user.Email, user.Username, request.Password));

        await _mediator.Publish(new UserCreated(user.Id, user.Email, user.Username), cancellationToken);

        return user.Id;
    }
}