using FluentAssertions;
using FluentValidation;
using NSubstitute;
using wf.Business.Validators;
using wf.Domain.Common;
using wf.Domain.Dto;
using Xunit;

namespace wf.Business.Tests.Validators;

public sealed class ForecastRequestValidatorTests
{
    private readonly ForecastRequestValidator _sut;

    private readonly IDateTimeProvider _dateTimeProviderMock = Substitute.For<IDateTimeProvider>();

    public ForecastRequestValidatorTests()
    {
        _sut = new ForecastRequestValidator(_dateTimeProviderMock);

        _dateTimeProviderMock.UtcNow.Returns(new DateTime(2024, 10, 15));
    }

    [Fact]
    public void Validate_ShouldSuccess_UnderValidCircumstances()
    {
        // Arrange
        var request = new ForecastRequest { City = "London", Date = new DateOnly(2024, 10, 16) };

        // Act
        Action act = () => _sut.ValidateAndThrow(request);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Validate_ShouldThrow_WhenEmptyCityProvided()
    {
        // Arrange
        var request = new ForecastRequest { City = "", Date = DateOnly.FromDateTime(DateTime.Now) };

        // Act
        Action act = () => _sut.ValidateAndThrow(request);

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Theory]
    [InlineData("2024-10-14", "yesterday")]
    [InlineData("2024-10-15", "today")]
    [InlineData("2024-10-20", "more than 5 days ahead")]
    public void Validate_ShouldThrow_WhenWrongDateProvided(string date, string becauseMessage)
    {
        // Arrange
        var request = new ForecastRequest { City = "London", Date = DateOnly.Parse(date) };

        // Act
        Action act = () => _sut.ValidateAndThrow(request);

        // Assert
        act.Should().Throw<ValidationException>($"'{becauseMessage} - but date should be more than today and not more than 5 days'");
    }
}
