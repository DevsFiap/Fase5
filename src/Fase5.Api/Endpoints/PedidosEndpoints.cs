using System.Security.Claims;
using Fase5.Application.Dtos.Pedido.Request;
using Fase5.Application.Interfaces;
using Fase5.Domain.Enuns;
using Microsoft.OpenApi.Models;

namespace Fase5.Api.Endpoints;

public static class PedidosEndpoints
{
    public static IEndpointRouteBuilder MapPedidosEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/pedidos")
                        .WithTags("Pedidos")
                        .RequireAuthorization();

        static int GetUserId(HttpContext ctx) =>
            int.Parse(ctx.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        #region Cliente Endpoints (POST / PATCH)

        group.MapPost("/", async (
            HttpContext http,
            CreatePedidoRequest dto,
            IPedidoAppService svc) =>
        {
            var clienteId = GetUserId(http);
            var resp = await svc.CriarAsync(clienteId, dto);
            return Results.Created($"/pedidos/{resp.Id}", resp);
        })
        .RequireAuthorization("cliente")
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Cria um novo pedido para o cliente logado."
        });

        group.MapPatch("/{id:int}/cancelar", async (
            HttpContext http,
            int id,
            CancelarPedidoRequest body,
            IPedidoAppService svc) =>
        {
            var clienteId = GetUserId(http);
            var resp = await svc.CancelarAsync(id, clienteId, body.Justificativa);
            return Results.Ok(resp);
        })
        .RequireAuthorization("cliente")
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Cancela um pedido existente, desde que ainda não tenha iniciado o preparo. Somente o cliente que criou o pedido pode cancelá-lo."
        });

        #endregion

        #region Cozinha / Gerente Endpoints (GET / PATCH)

        group.MapGet("/status/{status}", async (
            StatusPedido status,
            IPedidoAppService svc) =>
        {
            var lista = await svc.ListarPorStatusAsync(status);
            return Results.Ok(lista);
        })
        .RequireAuthorization("cozinha,gerente")
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Lista pedidos por status específico. Requer permissão de cozinha ou gerente."
        });

        group.MapPatch("/{id:int}/status", async (
            HttpContext http,
            int id,
            AtualizarStatusPedidoRequest body,
            IPedidoAppService svc) =>
        {
            var funcId = GetUserId(http);

            var resp = body.Aceitar
                ? await svc.AceitarAsync(id, funcId)
                : await svc.RejeitarAsync(id, funcId, body.Motivo ?? "Rejeitado");

            return Results.Ok(resp);
        })
        .RequireAuthorization("cozinha,gerente")
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Atualiza o status de um pedido (aceitar ou rejeitar). Requer permissão de cozinha ou gerente."
        });

        #endregion

        return app;
    }
}