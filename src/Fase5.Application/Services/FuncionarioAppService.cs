using AutoMapper;
using Fase5.Application.Dtos.Funcionario.Request;
using Fase5.Application.Dtos.Funcionario.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Domain.Interfaces.Security;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Application.Services;

public class FuncionarioAppService(
        IFuncionarioDomainService _dom,
        IPasswordHashService _hash,
        IMapper _map) : IFuncionarioAppService
{
    public async Task<int> CriarAsync(CreateFuncionarioRequest dto)
    {
        var entity = _map.Map<Funcionario>(dto);
        entity.Senha = _hash.Hash(dto.Senha);
        await _dom.AddAsync(entity);
        return entity.Id;
    }

    public async Task AtualizarAsync(int id, UpdateFuncionarioRequest dto)
    {
        var func = await _dom.GetByIdAsync(id)
                   ?? throw new KeyNotFoundException("Funcionário não encontrado.");

        _map.Map(dto, func);
        if (!string.IsNullOrWhiteSpace(dto.Senha))
            func.Senha = _hash.Hash(dto.Senha);

        await _dom.ModifyAsync(func);
    }

    public Task DeletarAsync(int id) => _dom.RemoveAsync(new Funcionario { Id = id });

    public async Task<FuncionarioResponse?> GetByIdAsync(int id) =>
        _map.Map<FuncionarioResponse>(await _dom.GetByIdAsync(id));

    public async Task<IEnumerable<FuncionarioResponse>> GetAllAsync() =>
        _map.Map<IEnumerable<FuncionarioResponse>>(await _dom.GetAllAsync());

    public async Task<IEnumerable<FuncionarioResponse>> BuscarPorCargoAsync(CargoFuncionario cargo) =>
        _map.Map<IEnumerable<FuncionarioResponse>>(await _dom.BuscarPorCargoAsync(cargo));
}