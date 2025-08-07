using BenchlyBackend.DataService.Repositories.Location;
using BenchlyBackend.DataService.Services.Location;
using BenchlyBackend.Models.Locations;
using Moq;

namespace BenchlyBackendTests.DataService.Services;
public sealed class LocationServiceTests : IDisposable
{
    private Mock<ILocationResource> LocationResourceMock { get; set; } = new();
    private LocationService? LocationService { get; set; }

    public LocationServiceTests()
    {
        LocationService = new(LocationResourceMock.Object);
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
        LocationResourceMock.Verify(x => x.GetLocationsForUsersMapViewAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()), Times.Once);
    }

    [Fact]
    public async Task LocationService_AddLocationAsync_CallsRepository_Correctly()
    {
        // Arrange
        var location = new Location();

        // Act

        // Location Service nevber going to be null so safe to use !
        await LocationService!.AddLocationAsync(location);

        // Assert
        LocationResourceMock.Verify(x => x.AddLocationAsync(It.IsAny<Location>()), Times.Once);
    }

    [Fact]
    public void LocationServicce_Errors_OnMissing_Resource_ConstructorArgument()
    {
        var expectedException = new ArgumentNullException("locationResource");

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
        LocationResourceMock.VerifyNoOtherCalls();
    }
}
