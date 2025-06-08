using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Services;

public interface IClienteDomainService : IBaseDomainService<Cliente, int>
{
    Task<Cliente?> ObterPorEmailAsync(string email);
    Task<Cliente?> ObterPorCpfAsync(string cpf);
    Task<bool> VerificarSenhaAsync(int id, string senha);
}