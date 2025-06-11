using Fase5.Domain.Enuns;

namespace Fase5.Application.Dtos.Funcionario.Response;

public record FuncionarioResponse(
    int Id,
    string Nome,
    string Email,
    CargoFuncionario Cargo);