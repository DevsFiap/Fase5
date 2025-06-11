using AutoMapper;
using Fase5.Application.Dtos.Pedido.Request;
using Fase5.Application.Dtos.Pedido.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Enuns;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Application.Services;

public class PedidoAppService(
        IPedidoDomainService _dom,
        IMapper _map) : IPedidoAppService
{
    public async Task<int> CriarPedidoAsync(int clienteId, CreatePedidoRequest dto)
    {
        var itens = dto.Itens.Select(i => (i.ProdutoId, i.Quantidade));
        var pedido = await _dom.CriarAsync(clienteId, itens, dto.Entrega);
        return pedido.Id;
    }

    public async Task<PedidoResponse?> GetByIdAsync(int id) =>
        _map.Map<PedidoResponse>(await _dom.GetByIdAsync(id));

    public async Task<IEnumerable<PedidoResponse>> ListarPorClienteAsync(int clienteId) =>
        _map.Map<IEnumerable<PedidoResponse>>(await _dom.ListarPorClienteAsync(clienteId));

    public async Task<IEnumerable<PedidoResponse>> ListarPorStatusAsync(StatusPedido s) =>
        _map.Map<IEnumerable<PedidoResponse>>(await _dom.ListarPorStatusAsync(s));

    public async Task<PedidoResponse> AceitarPedidoAsync(int id, int funcId) =>
        _map.Map<PedidoResponse>(await _dom.AceitarAsync(id, funcId));

    public async Task<PedidoResponse> RejeitarPedidoAsync(int id, int funcId, string motivo) =>
        _map.Map<PedidoResponse>(await _dom.RejeitarAsync(id, funcId, motivo));

    public async Task<PedidoResponse> CancelarPedidoAsync(int id, int cliId, string motivo) =>
        _map.Map<PedidoResponse>(await _dom.CancelarAsync(id, cliId, motivo));
}