using learning_center_webapi.Contexts.Security.Application.CommandServices;

namespace learning_center_webapi.Contexts.Security.Domain.Middleware;

public class AuthMiddleware
{    
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    
    public async Task InvokeAsync(HttpContext context,IJwtTokenService jwtTokenService)
    {

        var isAnonymousEndpoint = context.GetEndpoint()?.Metadata.GetMetadata<Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute>() != null;
        if (isAnonymousEndpoint)
        {
            await _next(context);;
            return;
        }
        
        
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
        if (token is null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: No token provided");
            return;
        }

        var user = await jwtTokenService.ValidateTokenAsync(token);
        
        
        if( user is null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Invalid token");
            return;
        }
                
        if (user is not null)
        {
            context.Items["User"] = user;
        }
        
        await _next(context);
    }
}