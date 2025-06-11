using Fase5.Domain.Enuns;

namespace Fase5.Application.Dtos.Funcionario.Request;

public record UpdateFuncionarioRequest(
    string? Nome,
    string? Email,
    string? Senha,
    CargoFuncionario? Cargo);