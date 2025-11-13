using Org.BouncyCastle.Crypto.Generators;

namespace learning_center_webapi.Contexts.Security.Application.CommandServices;

public class HashService : IHashService
{
    public string HashPassword(string password)
    {
       return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}