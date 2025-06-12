using Fase5.Application.Dtos.Pedido.Request;
using Fase5.Application.Dtos.Pedido.Response;
using Fase5.Domain.Enuns;

namespace Fase5.Application.Interfaces;

public interface IPedidoAppService
{
    Task<PedidoResponse> CriarAsync(int clienteId, CreatePedidoRequest dto);
    Task<IEnumerable<PedidoResponse>> ListarPorClienteAsync(int clienteId);
    Task<IEnumerable<PedidoResponse>> ListarPorStatusAsync(StatusPedido status);
    Task<PedidoResponse?> GetByIdAsync(int id);
    Task<PedidoResponse> AceitarAsync(int id, int funcionarioId);
    Task<PedidoResponse> RejeitarAsync(int id, int funcionarioId, string motivo);
    Task<PedidoResponse> CancelarAsync(int id, int clienteId, string motivo);
}