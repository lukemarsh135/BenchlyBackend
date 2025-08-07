using BenchlyBackend.DataService.Services.Location;
using BenchlyBackend.Models.Locations;
using Microsoft.AspNetCore.Mvc;

namespace BenchlyBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController(ILocationService locationService) : ControllerBase
{
    private readonly ILocationService _locationService = locationService
        ?? throw new ArgumentNullException(nameof(locationService));

    [HttpGet("search")]
    public async Task<ActionResult<List<Location>>> GetLocationsForUsersMapViewAsync(
        [FromQuery] double minLat,
        [FromQuery] double maxLat,
        [FromQuery] double minLng,
        [FromQuery] double maxLng)
    {
        //if (minLat > maxLat || minLng > maxLng)
        //{
        //    return BadRequest("Invalid bounding box.");
        //}

        //return Ok(await _locationService.GetLocationsForUsersMapViewAsync(minLat, maxLat, minLng, maxLng));
        throw new InvalidOperationException("test");
    }

    [HttpPost("")]
    public async Task<ActionResult<bool>> CreateLocationAsync([FromBody] Location location)
    {
        return Ok(await _locationService.AddLocationAsync(location));
    }
}