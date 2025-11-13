using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace learning_center_webapi.Contexts.Security.Application.CommandServices;

/// <summary>
/// Provides JWT token generation functionality.
/// </summary>
public class JWTEncrypter : IJWTEncrypter
{
    private readonly SymmetricSecurityKey _secretKey;

    public JWTEncrypter(IConfiguration configuration)
    {
        var secret = configuration["authentication:secretKey"] ?? throw new ArgumentNullException();
        var keyBytes = Encoding.UTF8.GetBytes(secret);
        _secretKey = new SymmetricSecurityKey(keyBytes);
    }

    public string GenerateToken(Guid userId, string username, string profile)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Sid, userId.ToString()),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, profile)
        };
        var credentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = credentials
        };
        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    public Task<string> GenerateTokenAsync(Guid userId, string username, string profile)
    {
        return Task.FromResult(GenerateToken(userId, username, profile));
    }
}