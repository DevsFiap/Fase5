using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Repositories;

public interface IClienteRepository : IBaseRepository<Cliente, int>
{
    Task<Cliente?> ObterPorEmailAsync(string email);
    Task<Cliente?> ObterPorCpfAsync(string cpf);

    Task<bool> EmailExisteAsync(string email);
    Task<bool> CpfExisteAsync(string cpf);
}