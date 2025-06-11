namespace Fase5.Application.Dtos.Login.Response;

public record LoginResponse(
    int Id,
    string Nome,
    string Role,
    string Token,
    DateTime ExpiraEm);