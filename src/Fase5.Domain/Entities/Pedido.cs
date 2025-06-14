using Fase5.Domain.Enuns;

namespace Fase5.Domain.Entities;

public class Pedido
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public TipoEntrega Entrega { get; set; }
    public StatusPedido Status { get; set; } = StatusPedido.Pendente;
    public string? JustificativaCancelamento { get; set; }

    public ICollection<ItemPedido> Itens { get; set; } = [];

    public decimal Total => Itens.Sum(i => i.PrecoUnitario * i.Quantidade);
}

public class ItemPedido
{
    public int PedidoId { get; set; }
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;

    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}