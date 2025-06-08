using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Services;

public interface IAuthDomainService
{
    Task<Funcionario?> AutenticarFuncionarioAsync(string email, string senha);
    Task<Cliente?> AutenticarClienteAsync(string login, string senha);
}