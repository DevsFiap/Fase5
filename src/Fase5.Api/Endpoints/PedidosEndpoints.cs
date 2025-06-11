using Fase5.Application.Dtos.Pedido.Request;
using Fase5.Application.Interfaces;
using Fase5.Domain.Enuns;

namespace Fase5.Api.Endpoints;

public static class PedidosEndpoints
{
    public static IEndpointRouteBuilder MapPedidosEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/pedidos")
                       .WithTags("Pedidos")
                       .RequireAuthorization();  // precisa de token

        /* ------------------ cliente ------------------ */
        group.MapPost("/", async (
            CreatePedidoRequest dto,
            int clienteId,                           // vindo do token? resolver via context
            IPedidoAppService svc) =>
        {
            var id = await svc.CriarPedidoAsync(clienteId, dto);
            return Results.Created($"/pedidos/{id}", new { id });
        })
        .RequireAuthorization("cliente");

        group.MapPatch("/{id:int}/cancelar", async (
            int id,
            int clienteId,
            CancelarPedidoRequest body,
            IPedidoAppService svc) =>
        {
            var resp = await svc.CancelarPedidoAsync(id, clienteId, body.Justificativa);
            return Results.Ok(resp);
        })
        .RequireAuthorization("cliente");

        /* ------------------ cozinha ------------------ */
        group.MapGet("/status/{status}", async (
            StatusPedido status,
            IPedidoAppService svc) =>
            Results.Ok(await svc.ListarPorStatusAsync(status)))
        .RequireAuthorization("cozinha,gerente");

        group.MapPatch("/{id:int}/status", async (
            int id,
            AtualizarStatusPedidoRequest body,
            int funcionarioId,          // virá do token (NameIdentifier)
            IPedidoAppService svc) =>
        {
            var resp = body.Aceitar
                ? await svc.AceitarPedidoAsync(id, funcionarioId)
                : await svc.RejeitarPedidoAsync(id, funcionarioId, body.Motivo ?? "Rejeitado");
            return Results.Ok(resp);
        })
        .RequireAuthorization("cozinha,gerente");

        return app;
    }
}