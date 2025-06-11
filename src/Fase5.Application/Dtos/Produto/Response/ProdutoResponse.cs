using Fase5.Domain.Enuns;

namespace Fase5.Application.Dtos.Produto.Response;

public record ProdutoResponse(
    int Id,
    string Nome,
    string Descricao,
    decimal Preco,
    CategoriaProduto Categoria,
    bool Disponivel);