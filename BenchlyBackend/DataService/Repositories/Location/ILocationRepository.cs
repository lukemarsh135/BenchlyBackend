using BenchlyBackend.Infrastructure.Attributes;

namespace BenchlyBackend.DataService.Repositories.Location;

/// <summary>
///     Location Repository.
/// </summary>
[Registration]
public interface ILocationRepository
{
    /// <summary>
    ///     Get all locations for all the locations within the user's specified map asynchronously.
    /// </summary>
    /// <returns></returns>
    Task<List<Models.Locations.Location>> GetLocationsForUsersMapViewAsync(double minLat, double maxLat, double minLng, double maxLng);

    /// <summary>
    ///     Add a new loation.
    /// </summary>
    /// <returns>true if the location was added successfully, false if not.</returns>
    Task<bool> AddLocationAsync(Models.Locations.Location location);
}