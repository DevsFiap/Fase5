using Fase5.Application.Dtos.Funcionario.Request;
using Fase5.Application.Interfaces;

namespace Fase5.Api.Endpoints;

public static class FuncionariosEndpoints
{
    public static IEndpointRouteBuilder MapFuncionariosEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/funcionarios")
                       .WithTags("Funcionarios")
                       .RequireAuthorization("gerente");

        group.MapPost("/", async (
            CreateFuncionarioRequest dto,
            IFuncionarioAppService svc) =>
        {
            var id = await svc.CriarAsync(dto);
            return Results.Created($"/funcionarios/{id}", new { id });
        });

        group.MapGet("/", async (IFuncionarioAppService svc) =>
            Results.Ok(await svc.GetAllAsync()));

        group.MapGet("/{id:int}", async (
            int id,
            IFuncionarioAppService svc) =>
        {
            var f = await svc.GetByIdAsync(id);
            return f is null ? Results.NotFound() : Results.Ok(f);
        });

        group.MapPut("/{id:int}", async (
            int id,
            UpdateFuncionarioRequest dto,
            IFuncionarioAppService svc) =>
        {
            await svc.AtualizarAsync(id, dto);
            return Results.NoContent();
        });

        group.MapDelete("/{id:int}", async (
            int id,
            IFuncionarioAppService svc) =>
        {
            await svc.DeletarAsync(id);
            return Results.NoContent();
        });

        return app;
    }
}