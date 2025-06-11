namespace Fase5.Application.Dtos.Cliente.Request;

public record UpdateClienteRequest(
    string? Nome,
    string? Email,
    string? Senha);