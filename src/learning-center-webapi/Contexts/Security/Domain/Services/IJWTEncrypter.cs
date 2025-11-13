namespace learning_center_webapi.Contexts.Security.Application.CommandServices;

public interface IJWTEncrypter
{
    Task<string> GenerateTokenAsync(Guid userId, string username, string profile);
}