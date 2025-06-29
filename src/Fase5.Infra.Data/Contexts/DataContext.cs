using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Infra.Data.Configuration;
using Microsoft.AspNetCore.Identity;
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

        //Criar o usuário Admin ao fazer a migration
        var admin = new Funcionario
        {
            Id = 1,
            Nome = "Administrador",
            Email = "admin@fasttech.com",
            Cargo = CargoFuncionario.Gerente
        };

        // gera a hash uma única vez
        var hasher = new PasswordHasher<Funcionario>();
        admin.Senha = hasher.HashPassword(admin, "admin");

        modelBuilder.Entity<Funcionario>().HasData(admin);
    }
}