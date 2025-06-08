using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Domain.Services;

public sealed class PedidoDomainService(IUnitOfWork uow)
    : BaseDomainService<Pedido, int>(uow.PedidoRepository), IPedidoDomainService
{
    public async Task<Pedido> CriarAsync(int clienteId,
                                         IEnumerable<(int produtoId, int qtd)> itens,
                                         TipoEntrega entrega)
    {
        await uow.BeginTransactionAsync();
        try
        {
            var produtos = await uow.ProdutoRepository
                                    .BuscarDisponiveisAsync();

            var mapped = itens.Select(i =>
            {
                var prod = produtos.First(p => p.Id == i.produtoId);
                return new ItemPedido
                {
                    ProdutoId = prod.Id,
                    Produto = prod,
                    Quantidade = i.qtd,
                    PrecoUnitario = prod.Preco
                };
            }).ToList();

            var pedido = new Pedido
            {
                ClienteId = clienteId,
                Entrega = entrega,
                Itens = mapped
            };

            await uow.PedidoRepository.CreateAsync(pedido);
            await uow.CommitAsync();
            return pedido;
        }
        catch
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /* ------- consultas -------- */
    public Task<IEnumerable<Pedido>> ListarPorClienteAsync(int clienteId) =>
        uow.PedidoRepository.ObterPorClienteAsync(clienteId);

    public Task<IEnumerable<Pedido>> ListarPorStatusAsync(StatusPedido status) =>
        uow.PedidoRepository.ObterPorStatusAsync(status);

    /* ------- fluxo da cozinha ------- */
    public async Task<Pedido> AceitarAsync(int id, int funcionarioId)
    {
        var p = await uow.PedidoRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Pedido não encontrado.");

        if (p.Status != StatusPedido.Pendente)
            throw new InvalidOperationException("Pedido não está pendente.");

        p.Status = StatusPedido.Aceito;
        await uow.PedidoRepository.UpdateAsync(p);
        await uow.SaveChangesAsync();
        return p;
    }

    public async Task<Pedido> RejeitarAsync(int id, int funcionarioId, string motivo)
    {
        var p = await uow.PedidoRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Pedido não encontrado.");

        p.Status = StatusPedido.Rejeitado;
        p.JustificativaCancelamento = motivo;
        await uow.PedidoRepository.UpdateAsync(p);
        await uow.SaveChangesAsync();
        return p;
    }

    /* ------- cancelamento pelo cliente ------- */
    public async Task<Pedido> CancelarAsync(int id, int clienteId, string motivo)
    {
        var p = await uow.PedidoRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Pedido não encontrado.");

        if (p.ClienteId != clienteId || p.Status != StatusPedido.Pendente)
            throw new InvalidOperationException("Não é possível cancelar.");

        p.Status = StatusPedido.Cancelado;
        p.JustificativaCancelamento = motivo;

        await uow.PedidoRepository.UpdateAsync(p);
        await uow.SaveChangesAsync();
        return p;
    }
}