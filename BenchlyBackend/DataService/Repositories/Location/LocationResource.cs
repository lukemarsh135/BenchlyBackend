using BenchlyBackend.Data;
using BenchlyBackend.Models.Tags;
using Microsoft.EntityFrameworkCore;

namespace BenchlyBackend.DataService.Repositories.Location;

public sealed class LocationResource(DatabaseContext dbContext) : ILocationResource
{
    private readonly DatabaseContext _dbContext = dbContext
        ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<List<Models.Locations.Location>> GetLocationsForUsersMapViewAsync(double minLat, double maxLat, double minLng, double maxLng)
    {
        if (minLat == 0 || maxLat == 0 ||
            minLng == 0 || maxLng == 0)
        {
            throw new InvalidOperationException("Invalid bounding box coordinates.");
        }

        return await _dbContext.Set<Models.Locations.Location>()
          .Where(l => l.Latitude >= minLat && l.Latitude <= maxLat &&
                      l.Longitude >= minLng && l.Longitude <= maxLng)
          .AsNoTracking()
          .ToListAsync();
    }

    public async Task<bool> AddLocationAsync(Models.Locations.Location location)
    {
        try
        {
            var existingTags = new List<Tag>();

            if (location.Tags is not null && location.Tags.Count != 0)
            {
                foreach (var tag in location.Tags)
                {
                    var existingTag = await _dbContext.Set<Tag>()
                        .FirstOrDefaultAsync(t => t.Title == tag.Title || t.Id == tag.Id);

                    if (existingTag != null)
                    {
                        existingTags.Add(existingTag);
                    }
                }
            }

            location.Tags = existingTags;

            await _dbContext.Set<Models.Locations.Location>().AddAsync(location);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }
}