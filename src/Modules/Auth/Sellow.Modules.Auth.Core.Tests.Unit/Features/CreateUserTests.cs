using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Sellow.Modules.Auth.Core.Auth;
using Sellow.Modules.Auth.Core.Entities;
using Sellow.Modules.Auth.Core.Features;
using Sellow.Modules.Auth.Core.Features.Exceptions;
using Sellow.Modules.Auth.Core.Repositories;
using Sellow.Modules.Auth.IntegrationEvents;
using Xunit;

namespace Sellow.Modules.Auth.Core.Tests.Unit.Features;

public sealed class CreateUserTests
{
    private readonly CreateUserHandler _handler;
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();

    public CreateUserTests()
    {
        _handler = new CreateUserHandler(Mock.Of<ILogger<CreateUserHandler>>(), _mediatorMock.Object,
            _userRepositoryMock.Object, Mock.Of<IAuthService>());
    }

    [Fact]
    public async Task
        Should_Throw_UserAlreadyExistsException_When_User_With_Given_Credentials_Already_Exists_In_The_Database()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.IsUnique(It.IsAny<User>(), default)).ReturnsAsync(false);
        var command = new CreateUser
        {
            Email = "existing@user.com",
            Username = "existingUser",
            Password = "password"
        };

        // Act
        var exception = await Record.ExceptionAsync(() => _handler.Handle(command, default));

        // Assert
        exception.Should().BeOfType<UserAlreadyExistsException>();
        
        _userRepositoryMock.Reset();
    }

    [Fact]
    public async Task Should_Create_A_New_User()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.IsUnique(It.IsAny<User>(), default)).ReturnsAsync(true);
        var command = new CreateUser
        {
            Email = "new@user.com",
            Username = "newUser",
            Password = "password"
        };

        // Act
        var result = await Record.ExceptionAsync(() => _handler.Handle(command, default));

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Should_Publish_A_UserCreated_Event()
    {
        // Arrange
        _userRepositoryMock.Setup(x => x.IsUnique(It.IsAny<User>(), default)).ReturnsAsync(true);
        var command = new CreateUser
        {
            Email = "new@user.com",
            Username = "newUser",
            Password = "password"
        };

        // Act
        await _handler.Handle(command, default);

        // Assert
        _mediatorMock.Verify(x => x.Publish(It.IsAny<UserCreated>(), default));
    }
}