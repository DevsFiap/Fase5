using Fase5.Application.Dtos.Horarios.Request;
using Fase5.Application.Dtos.Horarios.Response;

namespace Fase5.Application.Interfaces;

public interface IHorarioDisponivelAppService
{
    Task<Guid> CriarAsync(Guid medicoId, HorarioRequest dto);
    Task ExcluirAsync(Guid horarioId, Guid medicoId);
    Task<IEnumerable<HorarioResponse>> ListarPorMedicoAsync(Guid medicoId);
}