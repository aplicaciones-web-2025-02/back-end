using learning_center_webapi.Contexts.Security.Domain.Model.Entities;

namespace learning_center_webapi.Contexts.Security.Application.CommandServices;

public interface IJwtTokenService
{
    Task<string> GenerateTokenAsync(Guid userId, string username, string profile);
    Task<User> ValidateTokenAsync(string token);
}
