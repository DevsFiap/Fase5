using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Security;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Domain.Services;

public sealed class ClienteDomainService(IUnitOfWork uow, IPasswordHashService hash)
    : BaseDomainService<Cliente, int>(uow.ClienteRepository),
      IClienteDomainService
{
    public Task<Cliente?> ObterPorEmailAsync(string email)
        => uow.ClienteRepository.ObterPorEmailAsync(email);

    public Task<Cliente?> ObterPorCpfAsync(string cpf)
        => uow.ClienteRepository.ObterPorCpfAsync(cpf);

    public async Task<bool> VerificarSenhaAsync(int id, string senha)
    {
        var c = await GetByIdAsync(id);
        return c is not null && hash.Verify(c.Senha!, senha);
    }

    public override async Task AddAsync(Cliente entity)
    {
        entity.Senha = hash.Hash(entity.Senha!);          // regra extra
        await base.AddAsync(entity);                       // grava no DbContext
        await uow.CommitAsync();                          // persiste
    }

    public override async Task ModifyAsync(Cliente entity)
    {
        await base.ModifyAsync(entity);
        await uow.CommitAsync();
    }

    public override async Task RemoveAsync(Cliente entity)
    {
        await base.RemoveAsync(entity);
        await uow.CommitAsync();
    }
}