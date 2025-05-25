using Fase5.Application.Dtos.Pacientes.Request;
using Fase5.Application.Dtos.Pacientes.Response;

namespace Fase5.Application.Interfaces;

public interface IPacienteAppService
{
    Task<Guid> CriarPacienteAsync(CreatePacienteRequest dto);
    Task AtualizarPacienteAsync(Guid id, UpdatePacienteRequest dto);
    Task ExcluirPacienteAsync(Guid id);
    Task<PacienteResponse?> BuscarPacientePorIdAsync(Guid id);
    Task<IEnumerable<PacienteResponse>> BuscarTodosPacientesAsync();
    Task<PacienteResponse?> ObterPacientePorCpfAsync(string cpf);
}