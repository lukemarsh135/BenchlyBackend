namespace BenchlyBackend.DataService.Services.Review;

using BenchlyBackend.Models.Reviews;
using BenchlyBackend.DataService.Repositories.Review;

public sealed class ReviewService(IReviewRepository reviewRepository) : IReviewService
{
    private readonly IReviewRepository _reviewRepository = reviewRepository
        ?? throw new ArgumentNullException(nameof(reviewRepository));

    public async Task<bool> AddReviewAsync(Review review)
    {
        ArgumentNullException.ThrowIfNull(review);

        if (review.LocationId <= 0)
        {
            throw new InvalidOperationException("Invalid LocationId provided.");
        }

        return await _reviewRepository.AddReviewAsync(review);
    }
}