using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Fase5.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fase5.Infra.Data.Extensions;

/// <summary>
/// Classe de extensão para injeção de dependência do Entity Framework.
/// </summary>
public static class EntityFrameworkExtension
{
    /// <summary> 
    /// Método de extensão para registrar o Entity Framework no serviço de injeção de dependência. 
    /// </summary> 
    public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        // Adiciona o DataContext com a string de conexão do banco de dados
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Fase5Context")));

        // Adicionar injeção de dependência para o UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}