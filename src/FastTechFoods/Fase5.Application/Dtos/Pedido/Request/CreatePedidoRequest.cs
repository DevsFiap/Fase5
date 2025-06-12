using Fase5.Application.Dtos.ItensPedido.Request;
using Fase5.Domain.Enuns;

namespace Fase5.Application.Dtos.Pedido.Request;

public record CreatePedidoRequest(
    IEnumerable<ItemPedidoRequest> Itens,
    TipoEntrega Entrega);