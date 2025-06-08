using Fase5.Domain.Interfaces.Security;
using Fase5.Infra.Security.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fase5.Infra.Security.Services;

public sealed class TokenService(IOptions<JwtSettings> options) : ITokenService
{
    private readonly JwtSettings _jwt = options.Value;

    public string CreateToken(int id, string login, string role)
    {
        var key = Encoding.ASCII.GetBytes(_jwt.SecretKey!);
        var handler = new JwtSecurityTokenHandler();

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Name,           login),
            new Claim(ClaimTypes.Role,           role)
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(_jwt.ExpirationInHours),
            SigningCredentials = new SigningCredentials(
                                     new SymmetricSecurityKey(key),
                                     SecurityAlgorithms.HmacSha256Signature)
        };

        return handler.WriteToken(handler.CreateToken(descriptor));
    }
}