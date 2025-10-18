using BenchlyBackend.DataService.Repositories.Review;
using BenchlyBackend.DataService.Services.Review;
using Moq;

namespace BenchlyBackendTests.DataService.Services.Review;

using BenchlyBackend.Models.Reviews;

public sealed class ReviewServiceTests
{
    private readonly Mock<IReviewRepository> _mockReviewRepository = new();
    private readonly IReviewService _reviewService;

    public ReviewServiceTests()
    {
        _reviewService = new ReviewService(_mockReviewRepository.Object);
    }

    [Fact]
    public void ReviewService_ErrorsOnNull_ReviewRepository()
    {
        // Arrange
        var expected = "Value cannot be null. (Parameter 'reviewRepository')";

        // Act & Assert
        var error = Assert.Throws<ArgumentNullException>(() => new ReviewService(null!));
        Assert.Equal(expected, error.Message);
    }

    [Fact]
    public async Task AddReviewAsync_ErrorsOnNull_Review()
    {
        // Arrange
        var expected = "Value cannot be null. (Parameter 'review')";

        // Act & Assert
        var error = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _reviewService.AddReviewAsync(null!));
        Assert.Equal(expected, error.Message);
    }

    [Fact]
    public async Task AddReviewAsync_ErrorsOnInvalid_LocationId()
    {
        // Arrange
        var expected = "Invalid LocationId provided.";
        var review = new Review
        {
            LocationId = 0,
            Rating = 5,
            Comments = "Great place!"
        };

        // Act & Assert
        var error = await Assert.ThrowsAsync<InvalidOperationException>(async () => await _reviewService.AddReviewAsync(review));
        Assert.Equal(expected, error.Message);
    }

    [Fact]
    public async Task AddReviewAsync_CallsRepository_AddReviewAsync_Once()
    {
        // Arrange
        var review = new Review
        {
            LocationId = 1,
            Rating = 5,
            Comments = "Great place!"
        };

        _mockReviewRepository
            .Setup(repo => repo.AddReviewAsync(review))
            .ReturnsAsync(true);

        // Act
        var result = await _reviewService.AddReviewAsync(review);

        // Assert
        Assert.True(result);
        _mockReviewRepository.Verify(repo => repo.AddReviewAsync(review), Times.Once);
    }
}