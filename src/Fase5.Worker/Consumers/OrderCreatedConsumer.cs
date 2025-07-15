using Fase5.Domain.Enuns;
using Fase5.Domain.Events;
using Fase5.Domain.Interfaces.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Fase5.Worker.Consumers;

public sealed class OrderCreatedConsumer(IUnitOfWork uow, ILogger<OrderCreatedConsumer> log)
    : IConsumer<OrderCreated>
{
    public async Task Consume(ConsumeContext<OrderCreated> ctx)
    {
        log.LogInformation("⏩ Pedido {Id} recebido no worker", ctx.Message.PedidoId);

        var pedido = await uow.PedidoRepository.GetByIdAsync(ctx.Message.PedidoId);
        if (pedido is null)
        {
            log.LogWarning("Pedido {Id} não encontrado no banco de dados.", ctx.Message.PedidoId);
            return;
        }

        pedido.Status = StatusPedido.Pendente;

        await uow.PedidoRepository.UpdateAsync(pedido);
        await uow.CommitAsync();

        log.LogInformation("✅ Pedido {Id} atualizado para status Pendente e salvo.", ctx.Message.PedidoId);
    }
}