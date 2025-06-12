namespace Fase5.Application.Dtos.ItensPedido.Response;

public record ItemPedidoResponse(
    int ProdutoId,
    string ProdutoNome,
    decimal PrecoUnitario,
    int Quantidade);