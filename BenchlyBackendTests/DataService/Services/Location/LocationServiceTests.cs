using BenchlyBackend.DataService.Repositories.Location;
using BenchlyBackend.DataService.Services.Location;
using BenchlyBackend.Models.Locations;
using Moq;

namespace BenchlyBackendTests.DataService.Services;
public sealed class LocationServiceTests : IDisposable
{
    private Mock<ILocationRepository> LocationRepositoryMock { get; set; } = new();
    private LocationService? LocationService { get; set; }

    public LocationServiceTests()
    {
        LocationService = new(LocationRepositoryMock.Object);
    }

    [Fact]
    public async Task LocationService_GetLocationsForUsersMapViewAsync_CallsRepository_Correctly()
    {
        // Arrange
        double minLat = 10.0;
        double maxLat = 20.0;
        double minLng = 30.0;
        double maxLng = 40.0;

        // Act

        // Location Service nevber going to be null so safe to use !
        await LocationService!.GetLocationsForUsersMapViewAsync(minLat, maxLat, minLng, maxLng);

        // Assert
        LocationRepositoryMock.Verify(x => x.GetLocationsForUsersMapViewAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()), Times.Once);
    }

    [Fact]
    public async Task LocationService_GetLocationsForUsersMapViewAsync_Throws_InvalidOperationException_On_Invalid_Range()
    {
        // Arrange
        double minLat = 20.0;
        double maxLat = 10.0; // Invalid range
        double minLng = 30.0;
        double maxLng = 40.0;

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await LocationService!.GetLocationsForUsersMapViewAsync(minLat, maxLat, minLng, maxLng));
        Assert.Equal("Invalid latitude or longitude range provided.", ex.Message);
    }

    [Fact]
    public async Task LocationService_AddLocationAsync_CallsRepository_Correctly()
    {
        // Arrange
        var location = new Location
        {
            Latitude = 15,
            Longitude = 35
        };

        // Act

        // Location Service nevber going to be null so safe to use !
        await LocationService!.AddLocationAsync(location);

        // Assert
        LocationRepositoryMock.Verify(x => x.AddLocationAsync(It.IsAny<Location>()), Times.Once);
    }

    [Fact]
    public void LocationServicce_Errors_OnMissing_Repository_ConstructorArgument()
    {
        var expectedException = new ArgumentNullException("locationRepository");

        var ex = Assert.Throws<ArgumentNullException>(() => new LocationService(null!));

        Assert.Equal(expectedException.Message, ex.Message);
    }

    [Fact]
    public async Task LocationService_AddLocationAsync_Throws_ArgumentNullException_On_Null_Location()
    {
        // Arrange
        Location? location = default!;

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => LocationService!.AddLocationAsync(location));

        Assert.Equal("location", ex.ParamName);
    }

    public void Dispose()
    {
        LocationRepositoryMock.VerifyNoOtherCalls();
    }
}
