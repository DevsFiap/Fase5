using AutoMapper;
using Fase5.Application.Dtos.Cliente.Request;
using Fase5.Application.Dtos.Cliente.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Security;

public class ClienteAppService(
        IUnitOfWork _uow,
        IPasswordHashService _hash,
        IMapper _map) : IClienteAppService
{
    public async Task<ClienteResponse?> GetByIdAsync(int id)
    => _map.Map<ClienteResponse>(await _uow.ClienteRepository.GetByIdAsync(id));

    public async Task<IEnumerable<ClienteResponse>> GetAllAsync()
        => _map.Map<IEnumerable<ClienteResponse>>(await _uow.ClienteRepository.GetAllAsync());

    public async Task<ClienteResponse> CriarAsync(CreateClienteRequest dto)
    {
        await _uow.BeginTransactionAsync();
        try
        {
            if (await _uow.ClienteRepository.EmailExisteAsync(dto.Email))
                throw new InvalidOperationException("E-mail já cadastrado.");

            if (await _uow.ClienteRepository.CpfExisteAsync(dto.CPF))
                throw new InvalidOperationException("CPF já cadastrado.");

            var entity = _map.Map<Cliente>(dto);
            entity.Senha = _hash.Hash(dto.Senha);

            await _uow.ClienteRepository.CreateAsync(entity);
            await _uow.CommitAsync();

            return _map.Map<ClienteResponse>(entity);
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    public async Task<ClienteResponse> AtualizarAsync(int id, UpdateClienteRequest dto)
    {
        await _uow.BeginTransactionAsync();
        try
        {
            var cli = await _uow.ClienteRepository.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException("Cliente não encontrado.");

            _map.Map(dto, cli);

            if (!string.IsNullOrWhiteSpace(dto.Senha))
                cli.Senha = _hash.Hash(dto.Senha);

            await _uow.ClienteRepository.UpdateAsync(cli);
            await _uow.CommitAsync();

            return _map.Map<ClienteResponse>(cli);
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    public async Task DeletarAsync(int id)
    {
        await _uow.BeginTransactionAsync();
        try
        {
            await _uow.ClienteRepository.DeleteAsync(new Cliente { Id = id });
            await _uow.CommitAsync();
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }
}