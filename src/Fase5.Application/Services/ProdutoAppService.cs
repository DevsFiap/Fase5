using AutoMapper;
using Fase5.Application.Dtos.Produto.Request;
using Fase5.Application.Dtos.Produto.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Application.Services;

public class ProdutoAppService(
        IProdutoDomainService _dom,
        IMapper _map) : IProdutoAppService
{
    public async Task<int> CriarAsync(CreateProdutoRequest dto)
    {
        if (await _dom.NomeExisteAsync(dto.Nome))
            throw new InvalidOperationException("Produto já existe.");

        var p = _map.Map<Produto>(dto);
        await _dom.AddAsync(p);
        return p.Id;
    }

    public async Task AtualizarAsync(int id, UpdateProdutoRequest dto)
    {
        var p = await _dom.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        _map.Map(dto, p);
        await _dom.ModifyAsync(p);
    }

    public Task DeletarAsync(int id) => _dom.RemoveAsync(new Produto { Id = id });

    public async Task<ProdutoResponse?> GetByIdAsync(int id) =>
        _map.Map<ProdutoResponse>(await _dom.GetByIdAsync(id));

    public async Task<IEnumerable<ProdutoResponse>> GetDisponiveisAsync() =>
        _map.Map<IEnumerable<ProdutoResponse>>(await _dom.BuscarDisponiveisAsync());

    public async Task<IEnumerable<ProdutoResponse>> BuscarPorCategoriaAsync(CategoriaProduto cat) =>
        _map.Map<IEnumerable<ProdutoResponse>>(await _dom.BuscarPorCategoriaAsync(cat));
}