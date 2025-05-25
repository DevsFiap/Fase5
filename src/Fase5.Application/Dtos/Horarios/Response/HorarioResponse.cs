namespace Fase5.Application.Dtos.Horarios.Response;

public sealed record HorarioResponse(Guid Id, DateTime Inicio, DateTime Fim, bool Ocupado);