using AutoMapper;
using Fase5.Application.Dtos.Cliente.Request;
using Fase5.Application.Dtos.Cliente.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Security;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Application.Services;

public class ClienteAppService(
        IClienteDomainService _dom,
        IPasswordHashService _hash,
        IMapper _map) : IClienteAppService
{
    public async Task<int> CriarAsync(CreateClienteRequest dto)
    {
        var entity = _map.Map<Cliente>(dto);
        entity.Senha = _hash.Hash(dto.Senha);
        await _dom.AddAsync(entity);
        return entity.Id;
    }

    public async Task AtualizarAsync(int id, UpdateClienteRequest dto)
    {
        var cli = await _dom.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        _map.Map(dto, cli);
        if (!string.IsNullOrWhiteSpace(dto.Senha))
            cli.Senha = _hash.Hash(dto.Senha);
        await _dom.ModifyAsync(cli);
    }

    public Task DeletarAsync(int id) => _dom.RemoveAsync(new Cliente { Id = id });

    public async Task<ClienteResponse?> GetByIdAsync(int id) =>
        _map.Map<ClienteResponse>(await _dom.GetByIdAsync(id));

    public async Task<IEnumerable<ClienteResponse>> GetAllAsync() =>
        _map.Map<IEnumerable<ClienteResponse>>(await _dom.GetAllAsync());
}