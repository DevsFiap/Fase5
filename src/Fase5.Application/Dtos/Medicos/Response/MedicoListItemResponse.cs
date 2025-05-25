namespace Fase5.Application.Dtos.Medicos.Response;

public sealed record MedicoListItemResponse(
    Guid Id,
    string Nome,
    string CRM,
    string Especialidade,
    decimal ValorConsultaPadrao);