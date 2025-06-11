using Fase5.Application.Dtos.Cliente.Request;
using Fase5.Application.Dtos.Cliente.Response;

namespace Fase5.Application.Interfaces;

public interface IClienteAppService
{
    Task<int> CriarAsync(CreateClienteRequest dto);
    Task AtualizarAsync(int id, UpdateClienteRequest dto);
    Task DeletarAsync(int id);
    Task<ClienteResponse?> GetByIdAsync(int id);
    Task<IEnumerable<ClienteResponse>> GetAllAsync();
}