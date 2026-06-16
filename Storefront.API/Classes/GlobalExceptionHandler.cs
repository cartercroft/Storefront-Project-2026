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

            var (statusCode, title) = exception switch
            {
                ArgumentException => (StatusCodes.Status400BadRequest, "Bad Request"),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
            };

            // Create standard ProblemDetails to prevent leaking sensitive stack traces
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
