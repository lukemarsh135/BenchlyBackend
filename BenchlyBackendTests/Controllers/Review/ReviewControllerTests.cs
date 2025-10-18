using BenchlyBackend.Controllers.Review;
using BenchlyBackend.DataService.Services.Review;
using Moq;

namespace BenchlyBackendTests.Controllers.Review;

using BenchlyBackend.Models.Reviews;

public sealed class ReviewControllerTests
{
    private readonly Mock<IReviewService> _reviewServiceMock = new();
    private readonly ReviewController _reviewController;

    public ReviewControllerTests()
    {
        _reviewController = new ReviewController(_reviewServiceMock.Object);
    }

    [Fact]
    public async Task AddReviewAsync_CallsService_Correctly()
    {
        // Arrange
        Review review = new();

        // Act
        await _reviewController.AddReviewAsync(review);

        // Assert
        _reviewServiceMock.Verify(x => x.AddReviewAsync(It.IsAny<Review>()), Times.Once);
    }
}