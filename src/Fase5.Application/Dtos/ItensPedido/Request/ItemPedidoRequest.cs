namespace Fase5.Application.Dtos.ItensPedido.Request;

public record ItemPedidoRequest(
    int ProdutoId,
    int Quantidade);