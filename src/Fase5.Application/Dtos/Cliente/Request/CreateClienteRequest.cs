namespace Fase5.Application.Dtos.Cliente.Request;

public record CreateClienteRequest(
    string Nome,
    string Email,
    string Senha,
    string CPF);