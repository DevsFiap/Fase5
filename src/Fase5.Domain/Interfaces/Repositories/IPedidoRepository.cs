using Fase5.Domain.Core;
using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;

namespace Fase5.Domain.Interfaces.Repositories;

public interface IPedidoRepository : IBaseRepository<Pedido, int>
{
    Task<Pedido?> ObterDetalhadoAsync(int id);
    Task<IEnumerable<Pedido>> ObterPorClienteAsync(int clienteId);
    Task<IEnumerable<Pedido>> ObterPorStatusAsync(StatusPedido status);
}