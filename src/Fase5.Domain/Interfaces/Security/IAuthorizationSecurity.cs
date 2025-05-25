using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Security;

public interface IAuthorizationSecurity
{
    string CreateToken(Usuario login);
}