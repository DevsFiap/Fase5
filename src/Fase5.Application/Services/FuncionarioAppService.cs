using AutoMapper;
using Fase5.Application.Dtos.Funcionario.Request;
using Fase5.Application.Dtos.Funcionario.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Security;

namespace Fase5.Application.Services;

public class FuncionarioAppService(
        IUnitOfWork uow,
        IPasswordHashService hash,
        IMapper map) : IFuncionarioAppService
{
    public async Task<FuncionarioResponse?> GetByIdAsync(int id)
        => map.Map<FuncionarioResponse>(await uow.FuncionarioRepository.GetByIdAsync(id));

    public async Task<IEnumerable<FuncionarioResponse>> GetAllAsync()
        => map.Map<IEnumerable<FuncionarioResponse>>(await uow.FuncionarioRepository.GetAllAsync());

    public async Task<IEnumerable<FuncionarioResponse>> BuscarPorCargoAsync(CargoFuncionario c)
        => map.Map<IEnumerable<FuncionarioResponse>>(await uow.FuncionarioRepository.BuscarPorCargoAsync(c));

    public async Task<bool> ExisteGerenteAsync()
        => await uow.FuncionarioRepository.ExisteCargoAsync(CargoFuncionario.Gerente);

    public async Task<FuncionarioResponse> CriarAsync(CreateFuncionarioRequest dto)
    {
        await uow.BeginTransactionAsync();
        try
        {
            if (await uow.FuncionarioRepository.EmailExisteAsync(dto.Email))
                throw new InvalidOperationException("E-mail já usado.");

            var ent = map.Map<Funcionario>(dto);
            ent.Senha = hash.Hash(dto.Senha);

            await uow.FuncionarioRepository.CreateAsync(ent);
            await uow.CommitAsync();

            return map.Map<FuncionarioResponse>(ent);
        }
        catch
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    public async Task<FuncionarioResponse> AtualizarAsync(int id, UpdateFuncionarioRequest dto)
    {
        await uow.BeginTransactionAsync();
        try
        {
            var ent = await uow.FuncionarioRepository.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException();

            map.Map(dto, ent);
            if (!string.IsNullOrWhiteSpace(dto.Senha))
                ent.Senha = hash.Hash(dto.Senha);

            await uow.FuncionarioRepository.UpdateAsync(ent);
            await uow.CommitAsync();
            return map.Map<FuncionarioResponse>(ent);
        }
        catch { await uow.RollbackAsync(); throw; }
    }

    public async Task DeletarAsync(int id)
    {
        await uow.BeginTransactionAsync();
        try
        {
            await uow.FuncionarioRepository.DeleteAsync(new Funcionario { Id = id });
            await uow.CommitAsync();
        }
        catch { await uow.RollbackAsync(); throw; }
    }
}