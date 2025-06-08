using Fase5.Domain.Enuns;

namespace Fase5.Domain.Entities;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public decimal Preco { get; set; }
    public bool Disponivel { get; set; } = true;
    public CategoriaProduto Categoria { get; set; }
}