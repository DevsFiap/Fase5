using Fase5.Application.Dtos.Consultas.Request;
using Fase5.Application.Dtos.Consultas.Response;

namespace Fase5.Application.Interfaces;

public interface IConsultaAppService
{
    Task<IEnumerable<ConsultaResponse>> ObterConsultasPorMedicoIdAsync(Guid medicoId);
    Task<IEnumerable<ConsultaResponse>> ObterConsultasPorPacienteIdAsync(Guid pacienteId);
    Task<ConsultaResponse?> ObterConsultaPorIdAsync(Guid consultaId);

    Task<ConsultaResponse> AgendarAsync(AgendarConsultaRequest dto, Guid pacienteId);
    Task<ConsultaResponse> CancelarAsync(Guid consultaId, CancelarConsultaRequest dto, Guid pacienteId);
    Task<ConsultaResponse> AtualizarStatusAsync(Guid consultaId, AtualizarStatusConsultaRequest dto, Guid medicoId);
}