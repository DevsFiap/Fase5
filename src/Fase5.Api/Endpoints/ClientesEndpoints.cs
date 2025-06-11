using Fase5.Application.Dtos.Cliente.Request;
using Fase5.Application.Interfaces;

namespace Fase5.Api.Endpoints;

public static class ClientesEndpoints
{
    public static IEndpointRouteBuilder MapClientesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/clientes")
                       .WithTags("Clientes")
                       .RequireAuthorization(); // qualquer usuário logado

        group.MapPost("/", async (
            CreateClienteRequest dto,
            IClienteAppService svc) =>
        {
            var id = await svc.CriarAsync(dto);
            return Results.Created($"/clientes/{id}", new { id });
        })
        .AllowAnonymous();                        // cadastro público

        group.MapGet("/", async (IClienteAppService svc) =>
            Results.Ok(await svc.GetAllAsync()));

        group.MapGet("/{id:int}", async (int id, IClienteAppService svc) =>
            await svc.GetByIdAsync(id) is { } c ? Results.Ok(c) : Results.NotFound());

        group.MapPut("/{id:int}", async (
            int id, UpdateClienteRequest dto, IClienteAppService svc) =>
        {
            await svc.AtualizarAsync(id, dto);
            return Results.NoContent();
        });

        group.MapDelete("/{id:int}", async (int id, IClienteAppService svc) =>
        {
            await svc.DeletarAsync(id);
            return Results.NoContent();
        });

        return app;
    }
}