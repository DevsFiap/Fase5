using Fase5.Api.Endpoints;
using Fase5.Api.Extensions;
using Fase5.Application.Extensions;
using Fase5.Domain.Extensions;
using Fase5.Infra.Data.Extensions;
using Fase5.Infra.Security.Extensions;

var builder = WebApplication.CreateBuilder(args);

/* ---------- Infra básica ---------- */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* ---------- DI custom ---------- */
builder.Services.AddSwaggerDoc();
builder.Services.AddJwtBearer(builder.Configuration);
builder.Services.AddCorsPolicy();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddAppServices();
builder.Services.AddSecurityServices();

var app = builder.Build();

/* ---------- Middleware ---------- */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

//Minimal APIs
app.MapAuthEndpoints();
app.MapClientesEndpoints();
app.MapFuncionariosEndpoints();
app.MapProdutosEndpoints();
app.MapPedidosEndpoints();

app.Run();