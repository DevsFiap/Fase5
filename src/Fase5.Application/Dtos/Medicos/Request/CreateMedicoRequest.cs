namespace Fase5.Application.Dtos.Medicos.Request;

public sealed record CreateMedicoRequest(
    string Nome,
    string Login,
    string Senha,
    string CRM,
    string Especialidade,
    decimal ValorConsultaPadrao);