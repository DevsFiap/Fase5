using Fase5.Application.Dtos.Medicos.Request;
using Fase5.Application.Dtos.Medicos.Response;

namespace Fase5.Application.Interfaces;

public interface IMedicoAppService
{
    Task<IEnumerable<MedicoDetailResponse>> BuscarMedicosPorEspecialidadeAsync(string especialidade);
    Task<MedicoDetailResponse?> ObterPorCrmAsync(string crm);
    Task<MedicoDetailResponse?> BuscarMedicoPorIdAsync(Guid id);
    Task<IEnumerable<MedicoListItemResponse>> BuscarTodosMedicosAsync();


    Task<Guid> CriarMedicoAsync(CreateMedicoRequest dto);
    Task AtualizarMedicoAsync(Guid id, UpdateMedicoRequest dto);
    Task ExcluirMedicoAsync(Guid id);
}