namespace learning_center_webapi.Contexts.Security.Domain.Commands;

public class CreateUserCommand
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Profile { get; set; } = string.Empty;
}