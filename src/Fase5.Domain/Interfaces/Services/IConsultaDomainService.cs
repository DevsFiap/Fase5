using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Services;

public interface IConsultaDomainService : IBaseDomainService<Consulta, Guid>
{
    Task<Consulta?> ObterConsultaPorIdAsync(Guid id);
    Task<IEnumerable<Consulta>> ObterConsultasPorMedicoIdAsync(Guid medicoId);
    Task<IEnumerable<Consulta>> ObterConsultasPorPacienteIdAsync(Guid pacienteId);

    Task<Consulta> AgendarConsultaAsync(Guid medicoId, Guid pacienteId, DateTime dataHora);
    Task<Consulta> AtualizarStatusAsync(Guid consultaId, Guid medicoId, bool aceitar);
    Task<Consulta> CancelarConsultaAsync(Guid consultaId, Guid pacienteId, string justificativa);
}