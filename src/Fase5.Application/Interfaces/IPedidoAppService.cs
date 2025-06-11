using Fase5.Application.Dtos.Pedido.Request;
using Fase5.Application.Dtos.Pedido.Response;
using Fase5.Domain.Enuns;

namespace Fase5.Application.Interfaces;

public interface IPedidoAppService
{
    Task<int> CriarPedidoAsync(int clienteId, CreatePedidoRequest dto);
    Task<PedidoResponse?> GetByIdAsync(int id);
    Task<IEnumerable<PedidoResponse>> ListarPorClienteAsync(int clienteId);
    Task<IEnumerable<PedidoResponse>> ListarPorStatusAsync(StatusPedido status);

    Task<PedidoResponse> AceitarPedidoAsync(int id, int funcId);
    Task<PedidoResponse> RejeitarPedidoAsync(int id, int funcId, string motivo);
    Task<PedidoResponse> CancelarPedidoAsync(int id, int clienteId, string motivo);
}