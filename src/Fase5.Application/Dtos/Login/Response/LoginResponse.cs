namespace Fase5.Application.Dtos.Login.Response;

public sealed record LoginResponse(Guid Id, string Nome, string Perfil, string Token, DateTime ExpiraEm);