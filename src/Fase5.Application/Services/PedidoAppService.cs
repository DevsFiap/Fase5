using AutoMapper;
using Fase5.Application.Dtos.Pedido.Request;
using Fase5.Application.Dtos.Pedido.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Domain.Events;
using Fase5.Domain.Interfaces.Repositories;
using MassTransit;

namespace Fase5.Application.Services;

/// <summary>Orquestra criação e fluxo de pedidos.</summary>
public class PedidoAppService(
        IUnitOfWork uow,
        IMapper map,
        IPublishEndpoint _publish) : IPedidoAppService
{
    public async Task<PedidoResponse> CriarAsync(int clienteId, CreatePedidoRequest dto)
    {
        await uow.BeginTransactionAsync();
        try
        {
            // 1) Carrega produtos necessários
            var produtoIds = dto.Itens.Select(i => i.ProdutoId).ToList();
            var produtos = await uow.ProdutoRepository.BuscarDisponiveisAsync();
            var dict = produtos.Where(p => produtoIds.Contains(p.Id))
                                     .ToDictionary(p => p.Id);

            // 2) Mapeia itens
            var itens = dto.Itens.Select(i => new ItemPedido
            {
                ProdutoId = i.ProdutoId,
                Produto = dict[i.ProdutoId],
                Quantidade = i.Quantidade,
                PrecoUnitario = dict[i.ProdutoId].Preco
            }).ToList();

            // 3) Cria pedido
            var pedido = new Pedido
            {
                ClienteId = clienteId,
                Entrega = dto.Entrega,
                Itens = itens,
                Status = StatusPedido.Pendente
            };

            await uow.PedidoRepository.CreateAsync(pedido);
            await uow.CommitAsync();

            await _publish.Publish(new OrderCreated(pedido.Id, pedido.CriadoEm));

            return map.Map<PedidoResponse>(pedido);
        }
        catch
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /* -------------------- FLUXO COZINHA ------------------- */
    public async Task<PedidoResponse> AceitarAsync(int id, int funcId)
        => await AlterarStatusAsync(id, StatusPedido.Aceito, funcId);

    public async Task<PedidoResponse> RejeitarAsync(int id, int funcId, string motivo)
        => await AlterarStatusAsync(id, StatusPedido.Rejeitado, funcId, motivo);

    public async Task<PedidoResponse> CancelarAsync(int id, int cliId, string motivo)
        => await AlterarStatusAsync(id, StatusPedido.Cancelado, cliId, motivo, clienteCancela: true);

    private async Task<PedidoResponse> AlterarStatusAsync(
        int id, StatusPedido novoStatus, int userId, string? motivo = null, bool clienteCancela = false)
    {
        await uow.BeginTransactionAsync();
        try
        {
            var pedido = await uow.PedidoRepository.GetByIdAsync(id)
                         ?? throw new KeyNotFoundException("Pedido não encontrado.");

            if (clienteCancela && pedido.ClienteId != userId)
                throw new UnauthorizedAccessException("Pedido não pertence ao cliente.");

            pedido.Status = novoStatus;
            if (motivo is not null)
                pedido.JustificativaCancelamento = motivo;

            await uow.PedidoRepository.UpdateAsync(pedido);
            await uow.CommitAsync();

            return map.Map<PedidoResponse>(pedido);
        }
        catch
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /* -------------------- CONSULTAS ---------------------- */
    public async Task<IEnumerable<PedidoResponse>> ListarPorClienteAsync(int cid)
        => map.Map<IEnumerable<PedidoResponse>>(await uow.PedidoRepository.ObterPorClienteAsync(cid));

    public async Task<IEnumerable<PedidoResponse>> ListarPorStatusAsync(StatusPedido s)
        => map.Map<IEnumerable<PedidoResponse>>(await uow.PedidoRepository.ObterPorStatusAsync(s));

    public async Task<PedidoResponse?> GetByIdAsync(int id)
        => map.Map<PedidoResponse>(await uow.PedidoRepository.ObterDetalhadoAsync(id));
}