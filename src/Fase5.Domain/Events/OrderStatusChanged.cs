using Fase5.Domain.Enuns;

namespace Fase5.Domain.Events;

public record OrderStatusChanged(int PedidoId, StatusPedido Status);