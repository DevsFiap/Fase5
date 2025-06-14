using Fase5.Domain.Enuns;

namespace Fase5.Application.Dtos.Produto.Request;

public record UpdateProdutoRequest(
    string? Nome,
    string? Descricao,
    decimal? Preco,
    CategoriaProduto? Categoria,
    bool? Disponivel);