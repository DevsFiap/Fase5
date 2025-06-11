using Fase5.Application.Dtos.ItensPedido.Response;
using Fase5.Domain.Enuns;

namespace Fase5.Application.Dtos.Pedido.Response;

public record PedidoResponse(
    int Id,
    int ClienteId,
    DateTime CriadoEm,
    TipoEntrega Entrega,
    StatusPedido Status,
    IEnumerable<ItemPedidoResponse> Itens,
    decimal Total,
    string? JustificativaCancelamento);