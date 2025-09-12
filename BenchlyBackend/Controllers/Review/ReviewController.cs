using Microsoft.AspNetCore.Mvc;

namespace BenchlyBackend.Controllers.Review;

using BenchlyBackend.DataService.Services.Review;
using BenchlyBackend.Models.Reviews;

[ApiController]
[Route("[controller]")]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    private readonly IReviewService _reviewService = reviewService
        ?? throw new ArgumentNullException(nameof(reviewService));

    /// <summary>
    ///     Asynchronously creates a new review for a given location.
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    [HttpPost("")]
    public async Task<ActionResult<bool>> AddReviewAsync([FromBody] Review review)
    {
        return Ok(await _reviewService.AddReviewAsync(review));
    }
}