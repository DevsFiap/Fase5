using Fase5.Domain.Interfaces.Security;
using Fase5.Infra.Security.Services;
using Fase5.Infra.Security.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Fase5.Api.Extensions;

public static class JwtBearerExtension
{
    public static IServiceCollection AddJwtBearer(
        this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
        services.AddTransient<ITokenService, TokenService>();

        var secret = config.GetValue<string>("JwtSettings:SecretKey")!;
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        var token = ctx.Request.Headers["Authorization"]
                                                .FirstOrDefault()?
                                                .Split(' ')
                                                .Last();
                        ctx.Token = token;
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization();
        return services;
    }
}