using AutoMapper;
using Fase5.Application.Dtos.Pacientes.Request;
using Fase5.Application.Dtos.Pacientes.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Application.Services;

public class PacienteAppService(IPacienteDomainService _pacienteSrv, IMapper _mapper) : IPacienteAppService
{
    public async Task<Guid> CriarPacienteAsync(CreatePacienteRequest dto)
    {
        var paciente = _mapper.Map<Paciente>(dto);
        await _pacienteSrv.AddAsync(paciente);
        return paciente.Id;
    }

    public async Task AtualizarPacienteAsync(Guid id, UpdatePacienteRequest dto)
    {
        var paciente = await _pacienteSrv.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException("Paciente não encontrado.");

        _mapper.Map(dto, paciente);
        await _pacienteSrv.ModifyAsync(paciente);
    }


    public async Task ExcluirPacienteAsync(Guid id)
    {
        var paciente = await _pacienteSrv.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException("Paciente não encontrado.");

        await _pacienteSrv.RemoveAsync(paciente);
    }

    public async Task<PacienteResponse?> BuscarPacientePorIdAsync(Guid id)
        => _mapper.Map<PacienteResponse>(await _pacienteSrv.GetByIdAsync(id));

    public async Task<IEnumerable<PacienteResponse>> BuscarTodosPacientesAsync()
        => _mapper.Map<IEnumerable<PacienteResponse>>(await _pacienteSrv.GetAllAsync());

    public async Task<PacienteResponse?> ObterPacientePorCpfAsync(string cpf)
        => _mapper.Map<PacienteResponse>(await _pacienteSrv.ObterPacientePorCpfAsync(cpf));
}