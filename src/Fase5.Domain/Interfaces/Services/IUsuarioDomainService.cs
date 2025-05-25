using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Services;

public interface IUsuarioDomainService : IBaseDomainService<Usuario, Guid>
{
    Task<Usuario?> ObterPorCpfAsync(string cpf);
    Task<Usuario?> ObterPorCrmAsync(string crm);
    Task<bool> VerificarSenhaAsync(Usuario usuario, string senha);
}