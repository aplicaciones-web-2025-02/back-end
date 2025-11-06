using learning_center_webapi.Contexts.Tutorials.Domain.Exceptions;
using learning_center_webapi.Contexts.Enrolments.Domain.Model.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace learning_center_webapi.Contexts.Shared.Domain.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;
    private readonly IStringLocalizer _localizer;

    public ExceptionFilter(ILogger<ExceptionFilter> logger, IStringLocalizerFactory factory)
    {
        _logger = logger;
        _localizer = factory.Create("Tutorials.TutorialController", "learning_center_webapi");
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var (statusCode, message) = exception switch
        {
            TutorialNotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            DuplicateTutorialTitleException => (StatusCodes.Status409Conflict, exception.Message),
            TutorialNotExistExceptions => (StatusCodes.Status404NotFound, exception.Message),
            UserNotExistExceptions => (StatusCodes.Status404NotFound, exception.Message),
            ArgumentException => (StatusCodes.Status400BadRequest, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, _localizer["ErrorCreatingTutorial"].Value)
        };

        // Simulación de logeo según tipo de error
        if (statusCode == StatusCodes.Status500InternalServerError)
            _logger.LogError(exception, "Unhandled exception occurred.");
        else
            _logger.LogWarning(exception, "Handled domain exception: {Message}", message);

        context.Result = new JsonResult(new { message }) { StatusCode = statusCode };
        context.ExceptionHandled = true;
    }
}