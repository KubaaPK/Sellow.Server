using FluentAssertions;
using Sellow.Modules.Shared.Abstractions.SharedKernel.ValueObjects;
using Sellow.Modules.Shared.Abstractions.SharedKernel.ValueObjects.Exceptions;
using Xunit;

namespace Sellow.Modules.Shared.Abstractions.Tests.Unit.SharedKernel.ValueObjects;

public sealed class EmailTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("invalidemail")]
    [InlineData("invalid@email")]
    [InlineData("invalid @email.com")]
    [InlineData("invalid@ email.com")]
    public void Should_Throw_InvalidEmailException_When_Email_Is_Not_Valid(string email)
    {
        // Act
        var exception = Record.Exception(() => new Email(email));

        // Assert
        exception.Should().BeOfType<InvalidEmailException>();
    }

    [Fact]
    public void Should_Create_Email_Value_Object()
    {
        // Act
        var result = new Email("john@doe.com");

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be("john@doe.com");
    }
}