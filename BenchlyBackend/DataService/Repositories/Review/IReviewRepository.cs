namespace BenchlyBackend.DataService.Repositories.Review;

using BenchlyBackend.Infrastructure.Attributes;
using BenchlyBackend.Models.Reviews;

/// <summary>
///     Review Repository.
/// </summary>
[Registration]
public interface IReviewRepository
{
    /// <summary>
    ///     Asynchronously adds a review for a specific location.
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    Task<bool> AddReviewAsync(Review review);
}