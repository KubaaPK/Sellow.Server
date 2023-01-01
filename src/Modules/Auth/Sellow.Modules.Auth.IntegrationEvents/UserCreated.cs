using MediatR;

namespace Sellow.Modules.Auth.IntegrationEvents;

public sealed record UserCreated(Guid Id, string Email, string Username) : INotification;