namespace Fase5.Application.Dtos.Medicos.Request;

public sealed record UpdateMedicoRequest(
    string? Nome,
    string? Especialidade,
    decimal? ValorConsultaPadrao);