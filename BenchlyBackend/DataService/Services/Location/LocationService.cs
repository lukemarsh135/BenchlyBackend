using BenchlyBackend.DataService.Repositories.Location;

namespace BenchlyBackend.DataService.Services.Location;

public sealed class LocationService(ILocationResource locationResource) : ILocationService
{
    private readonly ILocationResource _locationResource = locationResource
        ?? throw new ArgumentNullException(nameof(locationResource));

    public async Task<List<Models.Locations.Location>> GetLocationsForUsersMapViewAsync(double minLat, double maxLat, double minLng, double maxLng)
        => await _locationResource.GetLocationsForUsersMapViewAsync(minLat, maxLat, minLng, maxLng);

    public async Task<bool> AddLocationAsync(Models.Locations.Location location)
    {
        ArgumentNullException.ThrowIfNull(location, nameof(location));

        return await _locationResource.AddLocationAsync(location);
    }
}