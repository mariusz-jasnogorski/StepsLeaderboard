using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace StepsLeaderboard.WebAPI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.LogError(context.Exception, "An unhandled exception occurred");

            // Create a problem details response
            var problemDetails = new ProblemDetails
            {
                Title = "An unexpected error occurred",
                Detail = context.Exception.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            // Indicate we handled it
            context.ExceptionHandled = true;
        }
    }
}
