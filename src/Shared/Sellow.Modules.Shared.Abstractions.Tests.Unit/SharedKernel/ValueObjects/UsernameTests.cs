using FluentAssertions;
using Sellow.Modules.Shared.Abstractions.SharedKernel.ValueObjects;
using Sellow.Modules.Shared.Abstractions.SharedKernel.ValueObjects.Exceptions;
using Xunit;

namespace Sellow.Modules.Shared.Abstractions.Tests.Unit.SharedKernel.ValueObjects;

public sealed class UsernameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("     ")]
    [InlineData("ab")]
    [InlineData("qwertyqwertyqwertyqwertyqwerty")]
    public void Should_Throw_InvalidUsernameException_When_Username_Is_Not_Valid(string username)
    {
        // Act
        var exception = Record.Exception(() => new Username(username));

        // Assert
        exception.Should().BeOfType<InvalidUsernameException>();
    }

    [Fact]
    public void Should_Create_A_Username_Value_Object()
    {
        // Act
        var result = new Username("johndoe22");
        
        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be("johndoe22");
    }
}