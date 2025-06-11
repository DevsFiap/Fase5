using Fase5.Application.Dtos.Produto.Request;
using Fase5.Application.Interfaces;

namespace Fase5.Api.Endpoints;

public static class ProdutosEndpoints
{
    public static IEndpointRouteBuilder MapProdutosEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/produtos")
                       .WithTags("Produtos");

        /* ----- aberto ao público (cliente) ----- */
        group.MapGet("/", async (IProdutoAppService svc) =>
            Results.Ok(await svc.GetDisponiveisAsync()));

        group.MapGet("/{id:int}", async (int id, IProdutoAppService svc) =>
            await svc.GetByIdAsync(id) is { } p ? Results.Ok(p) : Results.NotFound());

        /* ----- gerência (cargo = gerente) ----- */
        group.MapPost("/", async (CreateProdutoRequest dto, IProdutoAppService svc) =>
        {
            var id = await svc.CriarAsync(dto);
            return Results.Created($"/produtos/{id}", new { id });
        })
        .RequireAuthorization("gerente");

        group.MapPut("/{id:int}", async (
            int id, UpdateProdutoRequest dto, IProdutoAppService svc) =>
        {
            await svc.AtualizarAsync(id, dto);
            return Results.NoContent();
        })
        .RequireAuthorization("gerente");

        group.MapDelete("/{id:int}", async (int id, IProdutoAppService svc) =>
        {
            await svc.DeletarAsync(id);
            return Results.NoContent();
        })
        .RequireAuthorization("gerente");

        return app;
    }
}