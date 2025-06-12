using Fase5.Domain.Core;
using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;

namespace Fase5.Domain.Interfaces.Repositories;

public interface IProdutoRepository : IBaseRepository<Produto, int>
{
    Task<IEnumerable<Produto>> BuscarDisponiveisAsync();
    Task<IEnumerable<Produto>> BuscarPorCategoriaAsync(CategoriaProduto categoria);
    Task<Produto?> ObterDisponivelPorIdAsync(int id);
    Task<bool> NomeExisteAsync(string nome);
}