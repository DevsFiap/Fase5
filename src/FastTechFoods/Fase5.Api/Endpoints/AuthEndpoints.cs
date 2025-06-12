using Fase5.Application.Dtos.Login.Request;
using Fase5.Application.Interfaces;

namespace Fase5.Api.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (
            LoginRequest dto,
            ILoginAppService svc,
            CancellationToken ct) =>
        {
            var resp = await svc.LoginAsync(dto);
            return Results.Ok(resp);
        })
        .AllowAnonymous()
        .WithTags("Authorization")
        .WithSummary("Login único (CPF, e-mail ou CRM)");

        return app;
    }
}