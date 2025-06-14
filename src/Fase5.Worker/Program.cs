using Fase5.Domain.Extensions;
using Fase5.Infra.Data.Extensions;
using Fase5.Worker.Consumers;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Fase5.Worker;

public class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                //Garante que o appsettings.json seja carregado e variáveis de ambiente
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddEntityFramework(hostContext.Configuration);
                services.AddDomainServices();

                services.AddMassTransit(x =>
                {
                    x.AddConsumers(Assembly.GetExecutingAssembly());

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        //Obtém as configurações do RabbitMQ do IConfiguration
                        var rabbitMqConfig = hostContext.Configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>();
                        if (rabbitMqConfig == null)
                        {
                            //Fallback (apenas para avisar, pois o Docker Compose deve fornecer)
                            Console.WriteLine("Aviso: Seção 'RabbitMQ' não encontrada no appsettings. Usando valores padrão para host.");
                            rabbitMqConfig = new RabbitMqSettings { Host = "rabbitmq", UserName = "guest", Password = "guest", Port = 5672 };
                        }

                        cfg.Host(rabbitMqConfig.Host, "/", h =>
                        {
                            h.Username(rabbitMqConfig.UserName);
                            h.Password(rabbitMqConfig.Password);
                        });

                        cfg.ReceiveEndpoint("pedidos", e =>
                        {
                            e.PrefetchCount = 16;
                            e.ConfigureConsumer<OrderCreatedConsumer>(context);
                            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                        });
                    });
                });
            });
}

/// <summary>
/// Classe para mapear as configurações do appsettings.json
/// </summary>
public class RabbitMqSettings
{
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string UserName { get; set; } = "guest";
    public string Password { get; set; } = "guest";
}