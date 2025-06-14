using Fase5.Application.Dtos.Cliente.Request;
using Fase5.Application.Interfaces;
using Microsoft.OpenApi.Models;

namespace Fase5.Api.Endpoints;

public static class ClientesEndpoints
{
    public static IEndpointRouteBuilder MapClientesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/clientes")
                        .WithTags("Clientes")
                        .RequireAuthorization();

        #region POST Endpoints

        group.MapPost("/", async (
            CreateClienteRequest dto,
            IClienteAppService svc) =>
        {
            var id = await svc.CriarAsync(dto);
            return TypedResults.Created($"/clientes/{id}", new { id });
        })
        .AllowAnonymous()
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Cria um novo cliente."
        });

        #endregion

        #region GET Endpoints

        group.MapGet("/", async (IClienteAppService svc) =>
            Results.Ok(await svc.GetAllAsync()))
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Retorna todos os clientes cadastrados."
            });

        group.MapGet("/{id:int}", async (int id, IClienteAppService svc) =>
            await svc.GetByIdAsync(id) is { } c ? Results.Ok(c) : Results.NotFound())
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Retorna um cliente específico pelo ID."
            });

        #endregion

        #region PUT Endpoints

        group.MapPut("/{id:int}", async (
            int id, UpdateClienteRequest dto, IClienteAppService svc) =>
        {
            await svc.AtualizarAsync(id, dto);
            return Results.NoContent();
        })
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Atualiza os dados de um cliente existente."
        });

        #endregion

        #region DELETE Endpoints

        group.MapDelete("/{id:int}", async (int id, IClienteAppService svc) =>
        {
            await svc.DeletarAsync(id);
            return Results.NoContent();
        })
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Exclui um cliente pelo ID."
        });

        #endregion

        return app;
    }
}