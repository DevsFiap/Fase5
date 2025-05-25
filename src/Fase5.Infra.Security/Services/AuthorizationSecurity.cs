using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Security;
using Fase5.Infra.Security.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fase5.Infra.Security.Services;

public class AuthorizationSecurity : IAuthorizationSecurity
{
    private readonly JwtSettings _jwtSettings;

    public AuthorizationSecurity(IOptions<JwtSettings> jwtSettings)
        => _jwtSettings = jwtSettings.Value;

    public string CreateToken(Usuario user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey!);

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name,           user.Login!),
        new Claim(ClaimTypes.Role,           user.Perfil.ToString())
    };

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationInHours),
            SigningCredentials = new SigningCredentials(
                                    new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
}