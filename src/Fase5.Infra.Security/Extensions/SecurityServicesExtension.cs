using Fase5.Domain.Interfaces.Security;
using Fase5.Infra.Security.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fase5.Infra.Security.Extensions;

public static class SecurityServicesExtension
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHashService, PasswordHashService>();
        return services;
    }
}