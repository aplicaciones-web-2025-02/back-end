namespace learning_center_webapi.Contexts.Security.Interfaces.REST.Resources;

public class UserResource
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
}
