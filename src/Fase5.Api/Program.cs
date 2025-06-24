using Fase5.Api.Endpoints;
using Fase5.Api.Extensions;
using Fase5.Api.Middlewares;
using Fase5.Application.Extensions;
using Fase5.Domain.Extensions;
using Fase5.Infra.Data.Extensions;
using Fase5.Infra.Security.Extensions;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

/* ---------- DI custom ---------- */
builder.Services.AddSwaggerDoc();
builder.Services.AddJwtBearer(builder.Configuration);
builder.Services.AddCorsPolicy();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddAppServices();
builder.Services.AddSecurityServices();

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"] ?? "rabbitmq");
        cfg.ConfigureEndpoints(ctx);
    });
});

builder.Services.AddSingleton<IPublishEndpoint>(sp => sp.GetRequiredService<IBus>());
builder.Services.AddMassTransitHostedService();

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("gerente", p => p.RequireRole("gerente"));
    opt.AddPolicy("cozinha", p => p.RequireRole("cozinha", "gerente"));
    opt.AddPolicy("cliente", p => p.RequireRole("cliente"));
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerDoc(app.Environment);
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

//Endpoints
app.MapAuthEndpoints();
app.MapClientesEndpoints();
app.MapFuncionariosEndpoints();
app.MapProdutosEndpoints();
app.MapPedidosEndpoints();

app.Run();