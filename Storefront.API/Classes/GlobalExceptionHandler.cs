using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Storefront.API.Classes
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;   
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var logBuilder = new StringBuilder();
            logBuilder.AppendLine($"An unhandled exception has occurred. {exception.Message}");
            logBuilder.AppendLine($"Request Path: {httpContext.Request.Path}");
            logBuilder.AppendLine($"Stack Trace: {exception.StackTrace}");
            _logger.LogError(logBuilder.ToString());

            var (statusCode, title, details) = exception switch
            {
                ArgumentException => (StatusCodes.Status400BadRequest, "Bad Request", "Invalid request format."),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized", "You are not authorized to access this resource."),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error", "An unknown error occurred.")
            };

            // Create standard ProblemDetails to prevent leaking sensitive stack traces
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = details,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
