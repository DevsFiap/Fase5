using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Fase5.Infra.Data.Repositories;

public sealed class UnitOfWork(DataContext ctx) : IUnitOfWork
{
    private readonly DataContext _ctx = ctx;
    private IDbContextTransaction? _transaction;

    //Repositórios
    private IFuncionarioRepository? _funcRepo;
    public IFuncionarioRepository FuncionarioRepository
        => _funcRepo ??= new FuncionarioRepository(_ctx);

    private IClienteRepository? _cliRepo;
    public IClienteRepository ClienteRepository
        => _cliRepo ??= new ClienteRepository(_ctx);

    private IProdutoRepository? _prodRepo;
    public IProdutoRepository ProdutoRepository
        => _prodRepo ??= new ProdutoRepository(_ctx);

    private IPedidoRepository? _pedidoRepo;
    public IPedidoRepository PedidoRepository
        => _pedidoRepo ??= new PedidoRepository(_ctx);

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