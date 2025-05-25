using Fase5.Application.Interfaces;
using Fase5.Application.Mappings;
using Fase5.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fase5.Application.Extensions;

/// <summary>
/// Classe de extensão para configurar as injeções de dependência dos serviços de aplicação.
/// </summary>
public static class AppServicesExtension
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IConsultaAppService, ConsultaAppService>();
        services.AddScoped<IHorarioDisponivelAppService, HorarioDisponivelAppService>();
        services.AddScoped<IMedicoAppService, MedicoAppService>();
        services.AddScoped<IPacienteAppService, PacienteAppService>();
        services.AddScoped<IUsuarioAppService, UsuarioAppService>();

        //AutoMapper
        services.AddAutoMapper(typeof(DtoToEntityMap));

        return services;
    }
}