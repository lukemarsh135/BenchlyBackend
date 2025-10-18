using BenchlyBackend.Data;

namespace BenchlyBackend.DataService.Repositories.Review;

using BenchlyBackend.Models.Reviews;

public sealed class ReviewRepository(DatabaseContext dbContext, ILogger<ReviewRepository> logger) : IReviewRepository
{
    private readonly DatabaseContext _dbContext = dbContext
        ?? throw new ArgumentNullException(nameof(dbContext));

    private readonly ILogger<ReviewRepository> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));

    /// <inheritdoc/>
    public async Task<bool> AddReviewAsync(Review review)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            _logger.LogInformation("Added review: {Review}", review);

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to add review: {Review}", review);
            return false;
        }
    }
}