using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Services;

public interface IPacienteDomainService : IBaseDomainService<Paciente, Guid>
{
    Task<Paciente?> ObterPorCpfAsync(string cpf);
}