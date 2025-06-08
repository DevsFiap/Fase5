using Fase5.Domain.Core;
using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;

namespace Fase5.Domain.Interfaces.Services;

public interface IProdutoDomainService : IBaseDomainService<Produto, int>
{
    Task<IEnumerable<Produto>> BuscarDisponiveisAsync();
    Task<IEnumerable<Produto>> BuscarPorCategoriaAsync(CategoriaProduto cat);
    Task<bool> NomeExisteAsync(string nome);
}