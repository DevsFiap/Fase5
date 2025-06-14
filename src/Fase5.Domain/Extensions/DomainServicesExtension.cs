using Fase5.Domain.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Fase5.Domain.Extensions;

/// <summary>
/// Classe de extensão para configurar as injeções de dependência dos serviços de domínio.
/// </summary>
public static class DomainServicesExtension
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        //services.AddScoped<IAuthDomainService, AuthDomainService>();
        //services.AddScoped<IFuncionarioDomainService, FuncionarioDomainService>();
        //services.AddScoped<IClienteDomainService, ClienteDomainService>();
        //services.AddScoped<IProdutoDomainService, ProdutoDomainService>();
        //services.AddScoped<IPedidoDomainService, PedidoDomainService>();

        services.AddValidatorsFromAssemblyContaining<ClienteValidator>();

        return services;
    }
}