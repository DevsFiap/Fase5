using Fase5.Domain.Core;
using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;

namespace Fase5.Domain.Interfaces.Services;

public interface IPedidoDomainService : IBaseDomainService<Pedido, int>
{
    Task<Pedido> CriarAsync(int clienteId, IEnumerable<(int produtoId, int qtd)> itens,
                            TipoEntrega entrega);

    Task<IEnumerable<Pedido>> ListarPorClienteAsync(int clienteId);
    Task<IEnumerable<Pedido>> ListarPorStatusAsync(StatusPedido status);

    Task<Pedido> AceitarAsync(int pedidoId, int funcionarioId);
    Task<Pedido> RejeitarAsync(int pedidoId, int funcionarioId, string motivo);
    Task<Pedido> CancelarAsync(int pedidoId, int clienteId, string motivo);
}