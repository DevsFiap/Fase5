using AutoMapper;
using Fase5.Application.Dtos.Horarios.Request;
using Fase5.Application.Dtos.Horarios.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Application.Services;

public class HorarioDisponivelAppService(
        IHorarioDisponivelDomainService _horarioSrv,
        IMapper _mapper) : IHorarioDisponivelAppService
{
    public async Task<Guid> CriarAsync(Guid medicoId, HorarioRequest dto)
    {
        bool conflita = await _horarioSrv
            .ExisteChoqueDeHorarioAsync(medicoId, dto.Inicio, dto.Fim);

        if (conflita)
            throw new InvalidOperationException("Já existe horário nesse intervalo.");

        var horario = new HorarioDisponivel
        {
            Id = Guid.NewGuid(),
            MedicoId = medicoId,
            Inicio = dto.Inicio,
            Fim = dto.Fim,
            Ocupado = false
        };

        await _horarioSrv.AddAsync(horario);
        return horario.Id;
    }

    public async Task ExcluirAsync(Guid horarioId, Guid medicoId)
    {
        var horario = await _horarioSrv.GetByIdAsync(horarioId)
                      ?? throw new KeyNotFoundException("Horário não encontrado.");

        if (horario.MedicoId != medicoId)
            throw new UnauthorizedAccessException("Horário não pertence ao médico.");

        await _horarioSrv.RemoveAsync(horario);
    }

    public async Task<IEnumerable<HorarioResponse>> ListarPorMedicoAsync(Guid medicoId)
    {
        var lista = await _horarioSrv.GetAllAsync();
        var doMedico = lista.Where(h => h.MedicoId == medicoId);

        return _mapper.Map<IEnumerable<HorarioResponse>>(doMedico);
    }
}