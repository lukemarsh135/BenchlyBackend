using BenchlyBackend.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;

namespace BenchlyBackendTests.DataService.Middleware;
public sealed class GlobalErrorHandlerTests
{
    public static IEnumerable<object[]> ExceptionTestData =>
    [
        [new ArgumentException("Invalid input"), StatusCodes.Status400BadRequest],
        [new UnauthorizedAccessException("Not authorized"), StatusCodes.Status401Unauthorized],
        [new DbUpdateException("Database conflict", new Exception()), StatusCodes.Status409Conflict],
        [new Exception("Generic error"), StatusCodes.Status500InternalServerError]
    ];

    [Theory]
    [MemberData(nameof(ExceptionTestData))]
    public async Task Invoke_WhenExceptionThrown_ReturnsProperErrorResponse(Exception exception, int expectedStatusCode)
    {
        // Arrange
        var context = new DefaultHttpContext();
        var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        Task next(HttpContext _) => throw exception;

        var loggerMock = new Mock<ILogger<GlobalErrorHandler>>();
        var middleware = new GlobalErrorHandler(next, loggerMock.Object);

        // Act
        await middleware.Invoke(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();

        var responseJson = JsonSerializer.Deserialize<JsonElement>(responseBody);

        Assert.Equal(expectedStatusCode, context.Response.StatusCode);
        Assert.Equal(exception.Message, responseJson.GetProperty("errorMessage").GetString());
        Assert.Equal(context.TraceIdentifier, responseJson.GetProperty("traceId").GetString());

        loggerMock.Verify(x => x.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unhandled exception")),
            exception,
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
        Times.Once);
    }
}