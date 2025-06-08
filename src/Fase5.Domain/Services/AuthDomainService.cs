using Fase5.Domain.Entities;
using Fase5.Domain.Helpers;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Security;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Domain.Services;

public sealed class AuthDomainService(IUnitOfWork uow, IPasswordHashService hashSvc) : IAuthDomainService
{
    public async Task<Funcionario?> AutenticarFuncionarioAsync(string email, string senha)
    {
        var funcionario = await uow.FuncionarioRepository.ObterPorEmailAsync(email);
        return funcionario is not null &&
               !string.IsNullOrWhiteSpace(funcionario.Senha) &&
               hashSvc.Verify(funcionario.Senha, senha)
             ? funcionario : null;
    }

    public async Task<Cliente?> AutenticarClienteAsync(string login, string senha)
    {
        var repo = uow.ClienteRepository;

        var cliente = DocumentoHelper.CpfValido(login)
                 ? await repo.ObterPorCpfAsync(login)
                 : await repo.ObterPorEmailAsync(login);

        return cliente is not null &&
               !string.IsNullOrWhiteSpace(cliente.Senha) &&
               hashSvc.Verify(cliente.Senha, senha)
             ? cliente : null;
    }
}