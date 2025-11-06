using learning_center_webapi.Contexts.Tutorials.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace learning_center_webapi.Contexts.Shared.Domain.FIlters;

public class InternalServerExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        //Logear la excepción si es necesario

        if (context.Exception is  TutorialNotFoundException)
        {            
            context.HttpContext.Response.StatusCode = 404;
            context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new
            {
                message = "Tutorial not found."
            });

        }
        
        if ( context.Exception is  ArgumentException)
        {            
            context.HttpContext.Response.StatusCode = 409;
            context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new
            {
                message = "Unauthorized access."
            });

        }
        
        if (context.Exception is  DuplicateTutorialTitleException)
        {            
            context.HttpContext.Response.StatusCode = 407;
            context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new
            {
                message = "Bad request."
            });
        }

        if (context.Exception is not TutorialNotFoundException and not ArgumentException and not DuplicateTutorialTitleException and not DuplicateTutorialTitleException)  
        {

            context.HttpContext.Response.StatusCode = 500;
            context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new
            {
                message = "An internal server error occurred. Please try again later."
            });
        }
    }
}