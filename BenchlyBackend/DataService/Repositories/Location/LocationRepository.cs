using Microsoft.EntityFrameworkCore;

namespace BenchlyBackend.DataService.Repositories.Location;

using BenchlyBackend.Data;
using BenchlyBackend.Models.Locations;

public sealed class LocationRepository(DatabaseContext dbContext, ILogger<LocationRepository> logger) : ILocationRepository
{
    private readonly DatabaseContext _dbContext = dbContext
        ?? throw new ArgumentNullException(nameof(dbContext));

    private readonly ILogger<LocationRepository> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));

    public async Task<List<Models.Locations.Location>> GetLocationsForUsersMapViewAsync(double minLat, double maxLat, double minLng, double maxLng)
    {
        if (minLat == 0 || maxLat == 0 ||
            minLng == 0 || maxLng == 0)
        {
            throw new InvalidOperationException("Invalid bounding box coordinates.");
        }

        return await _dbContext.Locations
           .AsNoTracking()
           .Include(l => l.Tags)
           .Include(l => l.Reviews)
           .Where(l => l.Latitude >= minLat && l.Latitude <= maxLat &&
                       l.Longitude >= minLng && l.Longitude <= maxLng)
           .ToListAsync();
    }

    public async Task<bool> AddLocationAsync(Location location)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            _dbContext.Locations.Add(location);
            await _dbContext.SaveChangesAsync();
            transaction.Commit();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to add location: {Location}", location);
            return false;
        }
    }
}