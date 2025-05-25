namespace Fase5.Application.Dtos.Consultas.Request;


public sealed record AgendarConsultaRequest(
    Guid MedicoId,
    DateTime DataHora);