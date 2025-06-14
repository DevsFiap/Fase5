using Fase5.Application.Dtos.Funcionario.Request;
using Fase5.Application.Interfaces;
using Microsoft.OpenApi.Models;

namespace Fase5.Api.Endpoints;

public static class FuncionariosEndpoints
{
    public static IEndpointRouteBuilder MapFuncionariosEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/funcionarios")
                        .WithTags("Funcionarios");

        #region POST Endpoints

        group.MapPost("/", async (
            HttpContext http,
            CreateFuncionarioRequest dto,
            IFuncionarioAppService svc) =>
        {
            var existeGerente = await svc.ExisteGerenteAsync();

            // Se já existe um gerente, apenas gerentes podem criar novos funcionários.
            if (existeGerente &&
                !http.User.IsInRole("gerente"))
                return Results.Unauthorized();

            var resp = await svc.CriarAsync(dto);
            return Results.Created($"/funcionarios/{resp.Id}", resp);
        })
        .AllowAnonymous() // O delegate decide se autoriza ou não, baseado na existência de gerente.
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Cria um novo funcionário. Apenas o primeiro funcionário pode ser criado por qualquer um; os próximos exigem que o solicitante seja um gerente."
        });

        #endregion

        group.RequireAuthorization("gerente"); // A partir daqui, apenas gerentes podem acessar.

        #region GET Endpoints

        group.MapGet("/", async (IFuncionarioAppService svc) =>
            Results.Ok(await svc.GetAllAsync()))
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Retorna todos os funcionários cadastrados. Requer permissão de gerente."
            });

        group.MapGet("/{id:int}", async (
            int id, IFuncionarioAppService svc) =>
            await svc.GetByIdAsync(id) is { } f ? Results.Ok(f) : Results.NotFound())
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Retorna um funcionário específico pelo ID. Requer permissão de gerente."
            });

        #endregion

        #region PUT Endpoints

        group.MapPut("/{id:int}", async (
            int id, UpdateFuncionarioRequest dto, IFuncionarioAppService svc) =>
        {
            await svc.AtualizarAsync(id, dto);
            return Results.NoContent();
        })
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Atualiza os dados de um funcionário existente. Requer permissão de gerente."
        });

        #endregion

        #region DELETE Endpoints

        group.MapDelete("/{id:int}", async (
            int id, IFuncionarioAppService svc) =>
        {
            await svc.DeletarAsync(id);
            return Results.NoContent();
        })
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Exclui um funcionário pelo ID. Requer permissão de gerente."
        });

        #endregion

        return app;
    }
}