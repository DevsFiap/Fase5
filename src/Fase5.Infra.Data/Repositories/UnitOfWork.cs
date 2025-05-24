using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Fase5.Infra.Data.Repositories;

public class UnitOfWork(DataContext _dataContext) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    public void BeginTransaction()
        => _transaction = _dataContext.Database.BeginTransaction();

    public void Commit()
        => _transaction?.Commit();

    public void Rollback()
        => _transaction?.Rollback();

    public async Task SaveChangesAsync()
        => await _dataContext.SaveChangesAsync();

    public IConsultaRepository ConsultaRepository => new ConsultaRepository(_dataContext);

    public IHorarioDisponivelRepository HorarioDisponivelRepository => new HorarioDisponivelRepository(_dataContext);

    public IMedicoRepository MedicoRepository => new MedicoRepository(_dataContext);

    public IPacienteRepository PacienteRepository => new PacienteRepository(_dataContext);

    public IUsuarioRepository UsuarioRepository => new UsuarioRepository(_dataContext);

    public void Dispose()
    => _transaction.Dispose();
}