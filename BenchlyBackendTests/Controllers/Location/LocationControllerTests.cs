using BenchlyBackend.Controllers.Location;
using BenchlyBackend.DataService.Services.Location;
using Moq;

namespace BenchlyBackendTests.Controllers.Location;

using BenchlyBackend.Models.Locations;

public sealed class LocationControllerTests
{
    private readonly Mock<ILocationService> _locationServiceMock = new();
    private readonly LocationController _locationController;

    public LocationControllerTests()
    {
        _locationController = new LocationController(_locationServiceMock.Object);
    }

    [Fact]
    public async Task GetLocationsForUsersMapViewAsync_CallsService_Correctly()
    {
        // Arrange
        double minLat = 10.0;
        double maxLat = 20.0;
        double minLng = 30.0;
        double maxLng = 40.0;

        // Act
        await _locationController.GetLocationsForUsersMapViewAsync(minLat, maxLat, minLng, maxLng);

        // Assert
        _locationServiceMock.Verify(x => x.GetLocationsForUsersMapViewAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()), Times.Once);
    }

    [Fact]
    public async Task CreateLocationAsync_CallsService_Correctly()
    {
        // Arrange
        var location = new Location
        {
            Latitude = 15,
            Longitude = 35
        };

        // Act
        await _locationController.CreateLocationAsync(location);

        // Assert
        _locationServiceMock.Verify(x => x.AddLocationAsync(It.IsAny<Location>()), Times.Once);
    }
}