using learning_center_webapi.Contexts.Security.Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc.Filters;

namespace learning_center_webapi.Contexts.Tutorials.Domain.Attributes;

public class CustomAuthorizeAtrribute : Attribute, IAsyncAuthorizationFilter
{

    private readonly string[] _profiles;
    
    public CustomAuthorizeAtrribute(params string[] profiles)
    {
        _profiles = profiles[0].Split(",");
    }
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.Items["User"]  as User;

        if (user?.Profile is null || !_profiles.Contains(user.Profile))
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.HttpContext.Response.WriteAsync("Forbidden: You do not have permission to access this resource");
            return;
        }
    }
}