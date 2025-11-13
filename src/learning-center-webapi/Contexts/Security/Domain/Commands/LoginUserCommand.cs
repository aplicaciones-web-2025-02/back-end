namespace learning_center_webapi.Contexts.Security.Domain.Commands;

public class LoginUserCommand
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}