using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Repositories;

public interface IPacienteRepository : IBaseRepository<Paciente, Guid>
{
    Task<Paciente?> ObterPorCpfAsync(string cpf);
}