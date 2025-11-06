using learning_center_webapi.Contexts.Tutorials.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace learning_center_webapi.Contexts.Shared.Domain.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var (statusCode, message) = exception switch
        {
            TutorialNotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            ArgumentException => (StatusCodes.Status409Conflict, exception.Message),
            DuplicateTutorialTitleException => (StatusCodes.Status400BadRequest, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "An internal server error occurred. Please try again later.")
        };

        // Simulación de logeo según tipo de error
        if (statusCode == StatusCodes.Status500InternalServerError)
            _logger.LogError(exception, "Unhandled exception occurred.");
        else
            _logger.LogWarning(exception, "Handled domain exception: {Message}", message);

        context.Result = new JsonResult(new { message }) { StatusCode = statusCode };
    }
}