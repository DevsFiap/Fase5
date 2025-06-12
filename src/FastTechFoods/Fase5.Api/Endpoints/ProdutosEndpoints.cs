using Fase5.Application.Dtos.Produto.Request;
using Fase5.Application.Interfaces;
using Microsoft.OpenApi.Models;

namespace Fase5.Api.Endpoints;

public static class ProdutosEndpoints
{
    public static IEndpointRouteBuilder MapProdutosEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/produtos")
                        .WithTags("Produtos");

        #region Cliente Endpoints (GET - Abertos ao Público)

        group.MapGet("/", async (IProdutoAppService svc) =>
            Results.Ok(await svc.GetDisponiveisAsync()))
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Retorna todos os produtos disponíveis para venda."
            });

        group.MapGet("/{id:int}", async (int id, IProdutoAppService svc) =>
            await svc.GetByIdAsync(id) is { } p ? Results.Ok(p) : Results.NotFound())
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Retorna um produto específico pelo ID."
            });

        #endregion

        #region Gerência Endpoints (POST / PUT / DELETE - Requer Gerente)

        group.MapPost("/", async (CreateProdutoRequest dto, IProdutoAppService svc) =>
        {
            var id = await svc.CriarAsync(dto);
            return Results.Created($"/produtos/{id}", new { id });
        })
        .RequireAuthorization("gerente")
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Cria um novo produto. Requer permissão de gerente."
        });

        group.MapPut("/{id:int}", async (
            int id, UpdateProdutoRequest dto, IProdutoAppService svc) =>
        {
            await svc.AtualizarAsync(id, dto);
            return Results.NoContent();
        })
        .RequireAuthorization("gerente")
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Atualiza os dados de um produto existente pelo ID. Requer permissão de gerente."
        });

        group.MapDelete("/{id:int}", async (int id, IProdutoAppService svc) =>
        {
            await svc.DeletarAsync(id);
            return Results.NoContent();
        })
        .RequireAuthorization("gerente")
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Exclui um produto pelo ID. Requer permissão de gerente."
        });

        #endregion

        return app;
    }
}