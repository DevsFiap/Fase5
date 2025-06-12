using Fase5.Api.Middlewares.Models;
using Newtonsoft.Json;
using System.Net;

namespace Fase5.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
        => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized ||
            context.Response.StatusCode == (int)HttpStatusCode.Forbidden ||
            context.Response.StatusCode == (int)HttpStatusCode.NotFound)
        {
            if (context.Response.ContentLength == null || context.Response.ContentLength == 0)
            {
                context.Response.ContentType = "application/json";
                var statusCode = context.Response.StatusCode;
                string message;

                switch (statusCode)
                {
                    case (int)HttpStatusCode.Unauthorized:
                        message = "Acesso não autorizado. Credenciais inválidas ou ausentes.";
                        break;
                    case (int)HttpStatusCode.Forbidden:
                        message = "Acesso proibido. Você não tem permissão para acessar este recurso.";
                        break;
                    case (int)HttpStatusCode.NotFound:
                        message = "O recurso solicitado não foi encontrado.";
                        break;
                    default:
                        message = "Um erro inesperado ocorreu.";
                        break;
                }

                var errorResultModel = new ErrorResultModel
                {
                    StatusCode = statusCode,
                    Message = message
                };

                var jsonResponse = JsonConvert.SerializeObject(errorResultModel);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}