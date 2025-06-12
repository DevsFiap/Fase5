using Fase5.Infra.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Contexts;

/// <summary>
/// Classe de contexto para configuração do Entity Framework
/// </summary>
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClienteConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FuncionarioConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemPedidoConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidoConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProdutoConfiguration).Assembly);
    }
}