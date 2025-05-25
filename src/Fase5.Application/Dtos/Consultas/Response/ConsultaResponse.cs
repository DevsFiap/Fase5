namespace Fase5.Application.Dtos.Consultas.Response;

public sealed record ConsultaResponse(
    Guid Id,
    Guid MedicoId,
    string NomeMedico,
    Guid PacienteId,
    string NomePaciente,
    DateTime DataHora,
    decimal Valor,
    string Status,
    string? JustificativaCancelamento);