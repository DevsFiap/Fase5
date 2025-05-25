using AutoMapper;
using Fase5.Application.Dtos.Consultas.Request;
using Fase5.Application.Dtos.Consultas.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Application.Services;

public class ConsultaAppService(IConsultaDomainService _consultaSrv, IMapper _mapper) : IConsultaAppService
{
    public async Task<ConsultaResponse?> ObterConsultaPorIdAsync(Guid consultaId)
        => _mapper.Map<ConsultaResponse>(await _consultaSrv.ObterConsultaPorIdAsync(consultaId));

    public async Task<IEnumerable<ConsultaResponse>> ObterConsultasPorMedicoIdAsync(Guid medicoId)
        => _mapper.Map<IEnumerable<ConsultaResponse>>(await _consultaSrv.ObterConsultasPorMedicoIdAsync(medicoId));

    public async Task<IEnumerable<ConsultaResponse>> ObterConsultasPorPacienteIdAsync(Guid pacienteId)
        => _mapper.Map<IEnumerable<ConsultaResponse>>(await _consultaSrv.ObterConsultasPorPacienteIdAsync(pacienteId));

    public async Task<ConsultaResponse> AgendarAsync(AgendarConsultaRequest dto, Guid pacienteId)
    {
        var consulta = await _consultaSrv.AgendarConsultaAsync(dto.MedicoId, pacienteId, dto.DataHora);
        return _mapper.Map<ConsultaResponse>(consulta);
    }

    public async Task<ConsultaResponse> AtualizarStatusAsync(Guid consultaId, AtualizarStatusConsultaRequest dto, Guid medicoId)
    {
        var consulta = await _consultaSrv.AtualizarStatusAsync(consultaId, medicoId, dto.Aceitar);
        return _mapper.Map<ConsultaResponse>(consulta);
    }

    public async Task<ConsultaResponse> CancelarAsync(Guid consultaId, CancelarConsultaRequest dto, Guid pacienteId)
    {
        var consulta = await _consultaSrv.CancelarConsultaAsync(consultaId, pacienteId, dto.Justificativa);
        return _mapper.Map<ConsultaResponse>(consulta);
    }
}