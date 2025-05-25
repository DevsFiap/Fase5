using Fase5.Application.Dtos.Horarios.Response;

namespace Fase5.Application.Dtos.Medicos.Response;

public sealed record MedicoDetailResponse(
    Guid Id,
    string Nome,
    string CRM,
    string Especialidade,
    decimal ValorConsultaPadrao,
    IEnumerable<HorarioResponse> HorariosDisponiveis);