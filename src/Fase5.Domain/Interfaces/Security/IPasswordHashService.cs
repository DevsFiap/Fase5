namespace Fase5.Domain.Interfaces.Security;

public interface IPasswordHashService
{
    string Hash(string plainTextPassword);
    bool Verify(string hash, string plainTextPassword);
}