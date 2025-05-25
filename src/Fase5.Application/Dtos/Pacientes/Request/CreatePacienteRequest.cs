namespace Fase5.Application.Dtos.Pacientes.Request;

public sealed record CreatePacienteRequest(
    string Nome,
    string Login,
    string Senha,
    string CPF);