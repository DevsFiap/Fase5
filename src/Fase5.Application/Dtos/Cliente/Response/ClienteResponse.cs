namespace Fase5.Application.Dtos.Cliente.Response;

public record ClienteResponse(
    int Id,
    string Nome,
    string Email,
    string CPF);