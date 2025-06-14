namespace Fase5.Domain.Interfaces.Security;

public interface ITokenService
{
    string CreateToken(int id, string login, string role);
}