using BenchlyBackend.Data;
using BenchlyBackend.DataService.Repositories.Review;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace BenchlyBackendTests.DataService.Repositories.Review;

using BenchlyBackend.Models.Reviews;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore.Diagnostics;

public sealed class ReviewRepositoryTests
{
    private readonly Mock<ILogger<ReviewRepository>> _loggerMock = new();

    [Fact]
    public void ReviewRepository_Constructor_ShouldThrowArgumentNullException_WhenDbContextIsNull()
    {
        // Arrange
        var logger = _loggerMock.Object;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new ReviewRepository(null!, logger));
    }

    [Fact]
    public void ReviewRepository_Constructor_ShouldThrowArgumentNullException_WhenLoggerIsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("test_db")
            .Options;

        using var context = new DatabaseContext(options);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new ReviewRepository(context, null!));
    }

    [Fact]
    public async Task AddReviewAsync_ShouldAddReview()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("test_db")
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        var review = new Review()
        {
            Id = 1,
            LocationId = 2,
            Comments = "Smashing",
            Rating = 5,
            DatePosted = DateTime.UtcNow,
        };

        using var context = new DatabaseContext(options);

        var resource = new ReviewRepository(context, _loggerMock.Object);

        var result = await resource.AddReviewAsync(review);

        Assert.True(context.Reviews.Contains(review));
        Assert.True(result);
    }
}