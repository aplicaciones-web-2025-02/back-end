using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using learning_center_webapi.Contexts.Security.Domain.Model.Exceptions;

namespace learning_center_webapi.Contexts.Security.Application.CommandServices;

public class JwtTokenService : IJwtTokenService
{
    private readonly SymmetricSecurityKey _secretKey;

    public JwtTokenService(IConfiguration configuration)
    {
        var secretKey = configuration["authentication:secretKey"];
        if (string.IsNullOrWhiteSpace(secretKey))
            throw new MissingJwtSecretException();

        _secretKey = CreateSymmetricSecurityKey(secretKey);
    }

    private static SymmetricSecurityKey CreateSymmetricSecurityKey(string secretKey)
    {
        var keyBytes = Encoding.UTF8.GetBytes(secretKey);
        return new SymmetricSecurityKey(keyBytes);
    }

    public string GenerateToken(Guid userId, string username, string profile)
    {
        var claims = GetUserClaims(userId, username, profile);
        var signingCredentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = signingCredentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    private static Claim[] GetUserClaims(Guid userId, string username, string profile) => new[]
    {
        new Claim(ClaimTypes.Sid, userId.ToString()),
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, profile)
    };

    public Task<string> GenerateTokenAsync(Guid userId, string username, string profile) =>
        Task.FromResult(GenerateToken(userId, username, profile));
}
