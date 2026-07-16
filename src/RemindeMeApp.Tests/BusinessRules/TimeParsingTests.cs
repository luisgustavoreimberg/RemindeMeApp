using System;
using FluentAssertions;
using RemindeMeApp.Backend.Services;
using Xunit;

namespace RemindeMeApp.Tests.BusinessRules;

public class TimeParsingTests
{
    [Fact]
    public void ParseTimeToSeconds_ValidHHMM_ReturnsCorrectSeconds()
    {
        // Arrange
        var input = "01:30";

        // Act
        var result = TimeTrackingService.ParseTimeToSeconds(input);

        // Assert
        result.Should().Be(5400, "1 hora e 30 minutos equivale a 5400 segundos");
    }

    [Theory]
    [InlineData("XX:YY")]
    [InlineData("25:80")]
    [InlineData("invalid")]
    [InlineData("")]
    public void ParseTimeToSeconds_InvalidFormat_ThrowsArgumentException(string input)
    {
        // Act
        Action action = () => TimeTrackingService.ParseTimeToSeconds(input);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("*formato*");
    }
}
