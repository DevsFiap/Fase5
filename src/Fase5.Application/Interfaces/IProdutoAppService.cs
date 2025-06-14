using Fase5.Application.Dtos.Produto.Request;
using Fase5.Application.Dtos.Produto.Response;
using Fase5.Domain.Enuns;

namespace Fase5.Application.Interfaces;

public interface IProdutoAppService
{
    Task<ProdutoResponse> CriarAsync(CreateProdutoRequest dto);
    Task<ProdutoResponse> AtualizarAsync(int id, UpdateProdutoRequest dto);
    Task DeletarAsync(int id);
    Task<ProdutoResponse?> GetByIdAsync(int id);
    Task<IEnumerable<ProdutoResponse>> GetDisponiveisAsync();
    Task<IEnumerable<ProdutoResponse>> BuscarPorCategoriaAsync(CategoriaProduto cat);
}