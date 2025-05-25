using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

/// <summary>
/// Repositório de horários disponíveis do médico.
/// </summary>
public class HorarioDisponivelRepository(DataContext ctx)
    : BaseRepository<HorarioDisponivel, Guid>(ctx), IHorarioDisponivelRepository
{
    /// <summary>
    /// Retorna o slot que contém o instante <paramref name="dataHora"/>.
    /// Devolve null se não existir nenhum.
    /// </summary>
    public async Task<HorarioDisponivel?> ObterHorarioDisponivelAsync(Guid medicoId, DateTime dataHora)
    {
        return await ctx.Set<HorarioDisponivel>()
                        .FirstOrDefaultAsync(h =>
                               h.MedicoId == medicoId &&
                               h.Inicio <= dataHora &&
                               h.Fim >= dataHora);
    }

    /// <summary>
    /// Verifica se [início, fim] colide com algum outro horário já cadastrado.
    /// true  = há sobreposição (indisponível); false = livre.
    /// </summary>
    public async Task<bool> ExisteChoqueDeHorarioAsync(Guid medicoId, DateTime inicio, DateTime fim)
    {
        return await ctx.Set<HorarioDisponivel>()
                        .AnyAsync(h =>
                               h.MedicoId == medicoId &&
                               h.Inicio < fim &&
                               h.Fim > inicio);
    }
}