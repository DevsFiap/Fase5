using AutoMapper;
using Fase5.Application.Dtos.Produto.Request;
using Fase5.Application.Dtos.Produto.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Domain.Interfaces.Repositories;

namespace Fase5.Application.Services;

public class ProdutoAppService(IUnitOfWork uow, IMapper map) : IProdutoAppService
{
    public async Task<ProdutoResponse?> GetByIdAsync(int id)
        => map.Map<ProdutoResponse>(await uow.ProdutoRepository.GetByIdAsync(id));

    public async Task<IEnumerable<ProdutoResponse>> GetDisponiveisAsync()
        => map.Map<IEnumerable<ProdutoResponse>>(await uow.ProdutoRepository.BuscarDisponiveisAsync());

    public async Task<IEnumerable<ProdutoResponse>> BuscarPorCategoriaAsync(CategoriaProduto cat)
        => map.Map<IEnumerable<ProdutoResponse>>(await uow.ProdutoRepository.BuscarPorCategoriaAsync(cat));

    public async Task<ProdutoResponse> CriarAsync(CreateProdutoRequest dto)
    {
        await uow.BeginTransactionAsync();
        try
        {
            if (await uow.ProdutoRepository.NomeExisteAsync(dto.Nome))
                throw new InvalidOperationException("Produto já existe.");

            var ent = map.Map<Produto>(dto);
            await uow.ProdutoRepository.CreateAsync(ent);
            await uow.CommitAsync();
            return map.Map<ProdutoResponse>(ent);
        }
        catch { await uow.RollbackAsync(); throw; }
    }

    public async Task<ProdutoResponse> AtualizarAsync(int id, UpdateProdutoRequest dto)
    {
        await uow.BeginTransactionAsync();
        try
        {
            var ent = await uow.ProdutoRepository.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException();
            map.Map(dto, ent);
            await uow.ProdutoRepository.UpdateAsync(ent);
            await uow.CommitAsync();
            return map.Map<ProdutoResponse>(ent);
        }
        catch { await uow.RollbackAsync(); throw; }
    }

    public async Task DeletarAsync(int id)
    {
        await uow.BeginTransactionAsync();
        try
        {
            await uow.ProdutoRepository.DeleteAsync(new Produto { Id = id });
            await uow.CommitAsync();
        }
        catch { await uow.RollbackAsync(); throw; }
    }
}