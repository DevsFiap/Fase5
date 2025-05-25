using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Domain.Services;

public sealed class ConsultaDomainService(IUnitOfWork uow) : BaseDomainService<Consulta, Guid>(uow.ConsultaRepository), IConsultaDomainService
{
    public Task<Consulta?> ObterConsultaPorIdAsync(Guid id)
        => uow.ConsultaRepository.GetByIdAsync(id);

    public Task<IEnumerable<Consulta>> ObterConsultasPorMedicoIdAsync(Guid medicoId)
        => uow.ConsultaRepository.GetConsultasByMedicoIdAsync(medicoId);

    public Task<IEnumerable<Consulta>> ObterConsultasPorPacienteIdAsync(Guid pacienteId)
        => uow.ConsultaRepository.GetConsultasByPacienteIdAsync(pacienteId);

    public async Task<Consulta> AgendarConsultaAsync(Guid medicoId, Guid pacienteId, DateTime dataHora)
    {
        await uow.BeginTransactionAsync();
        try
        {
            var slot = await uow.HorarioDisponivelRepository.ObterHorarioDisponivelAsync(medicoId, dataHora);
            if (slot is null)
                throw new InvalidOperationException("Horário indisponível.");

            var consulta = new Consulta
            {
                Id = Guid.NewGuid(),
                MedicoId = medicoId,
                PacienteId = pacienteId,
                DataHora = dataHora,
                Valor = slot.Medico!.ValorConsultaPadrao,
                Status = StatusConsulta.Pendente
            };

            await uow.ConsultaRepository.CreateAsync(consulta);

            slot.Ocupado = true;
            await uow.HorarioDisponivelRepository.UpdateAsync(slot);

            await uow.CommitAsync();
            return consulta;
        }
        catch
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    public async Task<Consulta> AtualizarStatusAsync(Guid consultaId, Guid medicoId, bool aceitar)
    {
        var consulta = await uow.ConsultaRepository.GetByIdAsync(consultaId)
                       ?? throw new KeyNotFoundException("Consulta não encontrada.");

        if (consulta.MedicoId != medicoId)
            throw new UnauthorizedAccessException("Médico não pertence à consulta.");

        var slot = await uow.HorarioDisponivelRepository.ObterHorarioDisponivelAsync(medicoId, consulta.DataHora);

        if (slot is null)
            throw new InvalidOperationException("Horário não encontrado para a consulta.");

        consulta.Status = aceitar ? StatusConsulta.Aceita : StatusConsulta.Recusada;

        await uow.ConsultaRepository.UpdateAsync(consulta);
        await uow.CommitAsync();
        return consulta;
    }

    public async Task<Consulta> CancelarConsultaAsync(Guid consultaId, Guid pacienteId, string justificativa)
    {
        await uow.BeginTransactionAsync();
        try
        {
            var consulta = await uow.ConsultaRepository.GetByIdAsync(consultaId)
                           ?? throw new KeyNotFoundException("Consulta não encontrada.");

            if (consulta.PacienteId != pacienteId)
                throw new UnauthorizedAccessException("Paciente não pertence à consulta.");

            consulta.Status = StatusConsulta.Cancelada;
            consulta.JustificativaCancelamento = justificativa;

            await uow.ConsultaRepository.UpdateAsync(consulta);

            var slot = await uow.HorarioDisponivelRepository.ObterHorarioDisponivelAsync(consulta.MedicoId, consulta.DataHora);
            if (slot is not null)
            {
                slot.Ocupado = false;
                await uow.HorarioDisponivelRepository.UpdateAsync(slot);
            }

            await uow.CommitAsync();
            return consulta;
        }
        catch
        {
            await uow.RollbackAsync();
            throw;
        }
    }
}