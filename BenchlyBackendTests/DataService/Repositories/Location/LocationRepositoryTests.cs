using BenchlyBackend.Data;
using BenchlyBackend.DataService.Repositories.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;

namespace BenchlyBackendTests.DataService.Repositories.Location;

using BenchlyBackend.Models.Locations;

public sealed class LocationRepositoryTests
{
    private readonly Mock<ILogger<LocationRepository>> _logger = new();

    [Fact]
    public async Task GetLocationsForUsersMapViewAsync_ShouldThrowInvalidOperationException_WhenCoordinatesAreZero()
    {
       var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("test_db")
            .Options;

        using var context = new DatabaseContext(options);
        
        var repository = new LocationRepository(context, _logger.Object);

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await repository.GetLocationsForUsersMapViewAsync(0, 10, 0, 10));
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await repository.GetLocationsForUsersMapViewAsync(10, 0, 10, 10));
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await repository.GetLocationsForUsersMapViewAsync(10, 10, 0, 10));
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await repository.GetLocationsForUsersMapViewAsync(10, 10, 10, 0));
    }

    [Fact]
    public async Task GetLocationsForUsersMapViewAsync_ShouldReturnLocationsWithinBoundingBox()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("test_db")
            .Options;

        using var context = new DatabaseContext(options);
        
        context.Locations.AddRange(
            new Location { Id = 1, Latitude = 5, Longitude = 5 },
            new Location { Id = 2, Latitude = 15, Longitude = 15 },
            new Location { Id = 3, Latitude = 25, Longitude = 25 }
        );

        await context.SaveChangesAsync();

        var repository = new LocationRepository(context, _logger.Object);

        var locations = await repository.GetLocationsForUsersMapViewAsync(1, 20, 1, 20);

        Assert.Equal(2, locations.Count);
        Assert.Contains(locations, l => l.Id == 1);
        Assert.Contains(locations, l => l.Id == 2);
        Assert.DoesNotContain(locations, l => l.Id == 3);
    }

    [Fact]
    public async Task AddLocationAsync_ShouldAddLocationSuccessfully()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("test_db")
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)) // Needed so test doesnt fail due to no transaction support in InMemory DB
            .Options;

        using var context = new DatabaseContext(options);
        
        var repository = new LocationRepository(context, _logger.Object);

        var location = new Location
        {
            Latitude = 10,
            Longitude = 10,
            DistanceFromCurrentLocationMiles = 3.4,
            DateAdded = DateTime.UtcNow,
        };

        var result = await repository.AddLocationAsync(location);
        Assert.True(result);
        Assert.Equal(1, await context.Locations.CountAsync());
        Assert.Equal(3.4, (await context.Locations.FirstAsync()).DistanceFromCurrentLocationMiles);
    }

    [Fact]
    public void LocationRepository_ShouldThrow_WhenDbContextIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new LocationRepository(null!, _logger.Object));
    }

    [Fact]
    public void LocationRepository_ShouldThrow_WhenILoggerIsNull()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("test_db")
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)) // Needed so test doesnt fail due to no transaction support in InMemory DB
            .Options;

        using var context = new DatabaseContext(options);

        Assert.Throws<ArgumentNullException>(() => new LocationRepository(context,  null!));
    }
}