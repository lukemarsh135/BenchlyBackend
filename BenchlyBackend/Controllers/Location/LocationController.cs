using BenchlyBackend.DataService.Services.Location;
using Microsoft.AspNetCore.Mvc;

namespace BenchlyBackend.Controllers.Location;

using BenchlyBackend.Models.Locations;

[ApiController]
[Route("[controller]")]
public class LocationController(ILocationService locationService) : ControllerBase
{
    private readonly ILocationService _locationService = locationService
        ?? throw new ArgumentNullException(nameof(locationService));

    /// <summary>
    ///     Asynchronously retrieves a list of locations within the specified latitude and longitude bounds.
    /// </summary>
    /// <param name="minLat"></param>
    /// <param name="maxLat"></param>
    /// <param name="minLng"></param>
    /// <param name="maxLng"></param>
    /// <exception cref="InvalidOperationException"></exception>
    [HttpGet("search")]
    public async Task<ActionResult<List<Location>>> GetLocationsForUsersMapViewAsync(
        [FromQuery] double minLat, [FromQuery] double maxLat,
        [FromQuery] double minLng, [FromQuery] double maxLng)
    {
        return Ok(await _locationService.GetLocationsForUsersMapViewAsync(minLat, maxLat, minLng, maxLng));
    }

    /// <summary>
    ///     Asynchronously creates a new location.
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    [HttpPost("")]
    public async Task<ActionResult<bool>> CreateLocationAsync([FromBody] Location location)
    {
        return Ok(await _locationService.AddLocationAsync(location));
    }
}