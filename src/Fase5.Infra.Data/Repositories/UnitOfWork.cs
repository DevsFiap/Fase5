using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Fase5.Infra.Data.Repositories;

public sealed class UnitOfWork(DataContext ctx) : IUnitOfWork
{
    private readonly DataContext _ctx = ctx;
    private IDbContextTransaction? _transaction;

    //Repositórios
    private IConsultaRepository? _consultaRepo;
    public IConsultaRepository ConsultaRepository
        => _consultaRepo ??= new ConsultaRepository(_ctx);

    private IHorarioDisponivelRepository? _horarioRepo;
    public IHorarioDisponivelRepository HorarioDisponivelRepository
        => _horarioRepo ??= new HorarioDisponivelRepository(_ctx);

    public IMedicoRepository MedicoRepository => _medicoRepo ??= new MedicoRepository(_ctx);
    private IMedicoRepository? _medicoRepo;

    public IPacienteRepository PacienteRepository => _pacienteRepo ??= new PacienteRepository(_ctx);
    private IPacienteRepository? _pacienteRepo;

    public IUsuarioRepository UsuarioRepository => _usuarioRepo ??= new UsuarioRepository(_ctx);
    private IUsuarioRepository? _usuarioRepo;

    //Transações
    public async Task BeginTransactionAsync()
        => _transaction = await _ctx.Database.BeginTransactionAsync();

    public async Task CommitAsync()
    {
        await _ctx.SaveChangesAsync();
        if (_transaction is not null)
            await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction is not null)
            await _transaction.RollbackAsync();
    }

    public async Task SaveChangesAsync() 
        => await _ctx.SaveChangesAsync();


    public void Dispose()
    {
        _transaction?.Dispose();
        _ctx.Dispose();
    }
}