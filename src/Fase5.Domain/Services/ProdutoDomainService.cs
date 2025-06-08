using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Domain.Services;

public sealed class ProdutoDomainService(IUnitOfWork uow)
    : BaseDomainService<Produto, int>(uow.ProdutoRepository),
      IProdutoDomainService
{
    public Task<IEnumerable<Produto>> BuscarDisponiveisAsync()
        => uow.ProdutoRepository.BuscarDisponiveisAsync();

    public Task<IEnumerable<Produto>> BuscarPorCategoriaAsync(CategoriaProduto cat)
        => uow.ProdutoRepository.BuscarPorCategoriaAsync(cat);

    public Task<bool> NomeExisteAsync(string nome)
        => uow.ProdutoRepository.NomeExisteAsync(nome);
}