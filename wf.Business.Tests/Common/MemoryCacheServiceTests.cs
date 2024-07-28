using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using wf.Business.Common;
using wf.Domain.Dto;
using Xunit;

namespace wf.Business.Tests.Common;

public sealed class MemoryCacheServiceTests
{
    private readonly MemoryCacheService _sut;

    private readonly IMemoryCache _memoryCacheMock = Substitute.For<IMemoryCache>();

    public MemoryCacheServiceTests()
    {
        _sut = new MemoryCacheService(_memoryCacheMock);
    }

    [Fact]
    public async Task GetOrSet_ShouldReturnCorrectDataFromResolver_WhenNoCachedEntry()
    {
        // Arrange
        var response = new ForecastResponse { Temperature = 42 };
        var cacheKey = "cacheKey";
        var resolver = new Func<Task<ForecastResponse>>(() => Task.FromResult(response));
        _memoryCacheMock.TryGetValue(cacheKey, out _).Returns(false);

        // Act
        var result = await _sut.GetOrSet(cacheKey, resolver);

        // Assert
        result!.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task GetOrSet_ShouldReturnCorrectDataFromCache_WhenCachedEntry()
    {
        // Arrange
        var resolverResponse = new ForecastResponse { Temperature = 42 };
        var cacheKey = "cacheKey";
        var cachedResponse = new ForecastResponse { Temperature = 43 };
        var resolver = new Func<Task<ForecastResponse>>(() => Task.FromResult(resolverResponse));
        _memoryCacheMock.TryGetValue(cacheKey, out Arg.Any<string>()!).Returns(x =>
        {
            x[1] = JsonSerializer.Serialize(cachedResponse);
            return true;
        });

        // Act
        var result = await _sut.GetOrSet(cacheKey, resolver);

        // Assert
        result!.Should().BeEquivalentTo(cachedResponse);
    }
}
