using Fase5.Domain.Interfaces.Services;
using Fase5.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fase5.Domain.Extensions;

/// <summary>
/// Classe de extensão para configurar as injeções de dependência dos serviços de domínio.
/// </summary>
public static class DomainServicesExtension
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IConsultaDomainService, ConsultaDomainService>();
        services.AddScoped<IHorarioDisponivelDomainService, HorarioDisponivelDomainService>();
        services.AddScoped<IMedicoDomainService, MedicoDomainService>();
        services.AddScoped<IPacienteDomainService, PacienteDomainService>();
        services.AddScoped<IUsuarioDomainService, UsuarioDomainService>();

        return services;
    }
}