namespace BenchlyBackend.DataService.Services.Review;

using BenchlyBackend.Infrastructure.Attributes;
using BenchlyBackend.Models.Reviews;

/// <summary>
///     Review Service.
/// </summary>
[Registration]
public interface IReviewService
{
    /// <summary>
    ///     Asynchronously adds a new review.
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    Task<bool> AddReviewAsync(Review review);
}