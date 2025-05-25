using AutoMapper;
using Fase5.Application.Dtos.Medicos.Request;
using Fase5.Application.Dtos.Medicos.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Application.Services;

public class MedicoAppService(IMedicoDomainService _medicoSrv, IMapper _mapper) : IMedicoAppService
{
    public async Task<Guid> CriarMedicoAsync(CreateMedicoRequest dto)
    {
        var medico = _mapper.Map<Medico>(dto);
        await _medicoSrv.AddAsync(medico);
        return medico.Id;
    }

    public async Task AtualizarMedicoAsync(Guid id, UpdateMedicoRequest dto)
    {
        var medico = await _medicoSrv.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException("Médico não encontrado.");

        _mapper.Map(dto, medico);
        await _medicoSrv.ModifyAsync(medico);
    }

    public async Task ExcluirMedicoAsync(Guid id)
    {
        var medico = await _medicoSrv.GetByIdAsync(id)
                     ?? throw new KeyNotFoundException("Médico não encontrado.");

        await _medicoSrv.RemoveAsync(medico);
    }

    public async Task<MedicoDetailResponse?> BuscarMedicoPorIdAsync(Guid id)
        => _mapper.Map<MedicoDetailResponse>(await _medicoSrv.GetByIdAsync(id));

    public async Task<IEnumerable<MedicoListItemResponse>> BuscarTodosMedicosAsync()
        => _mapper.Map<IEnumerable<MedicoListItemResponse>>(await _medicoSrv.GetAllAsync());

    public async Task<IEnumerable<MedicoDetailResponse>> BuscarMedicosPorEspecialidadeAsync(string especialidade)
        => _mapper.Map<IEnumerable<MedicoDetailResponse>>(await _medicoSrv.BuscarPorEspecialidadeAsync(especialidade));

    public async Task<MedicoDetailResponse?> ObterPorCrmAsync(string crm)
        => _mapper.Map<MedicoDetailResponse>(await _medicoSrv.ObterPorCrmAsync(crm));
}