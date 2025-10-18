using BenchlyBackend.DataService.Repositories.Location;

namespace BenchlyBackend.DataService.Services.Location;

using BenchlyBackend.Models.Locations;

public sealed class LocationService(ILocationRepository locationRepository) : ILocationService
{
    private readonly ILocationRepository _locationRepository = locationRepository
        ?? throw new ArgumentNullException(nameof(locationRepository));

    /// <inheritdoc/>
    public async Task<List<Location>> GetLocationsForUsersMapViewAsync(double minLat, double maxLat, double minLng, double maxLng)
    {
        if (minLat > maxLat || minLng > maxLng)
        {
            throw new InvalidOperationException("Invalid latitude or longitude range provided.");
        }

        return await _locationRepository.GetLocationsForUsersMapViewAsync(minLat, maxLat, minLng, maxLng);
    }

    /// <inheritdoc/>
    public async Task<bool> AddLocationAsync(Location location)
    {
        ArgumentNullException.ThrowIfNull(location, nameof(location));

        if (location.Latitude <= 0 || location.Longitude <= 0)
        {
            throw new InvalidOperationException("Invalid latitude or longitude range provided.");
        }

        return await _locationRepository.AddLocationAsync(location);
    }
}