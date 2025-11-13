namespace learning_center_webapi.Contexts.Security.Application.CommandServices;

public interface IHashService
{
    string HashPassword(string password);
    
    bool VerifyPassword(string password, string hashedPassword);
}