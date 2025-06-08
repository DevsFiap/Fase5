using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Security;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Domain.Services;

public sealed class FuncionarioDomainService(IUnitOfWork uow, IPasswordHashService hash)
    : BaseDomainService<Funcionario, int>(uow.FuncionarioRepository),
      IFuncionarioDomainService
{
    public Task<Funcionario?> ObterPorEmailAsync(string email)
        => uow.FuncionarioRepository.ObterPorEmailAsync(email);

    public Task<IEnumerable<Funcionario>> BuscarPorCargoAsync(CargoFuncionario cargo)
        => uow.FuncionarioRepository.BuscarPorCargoAsync(cargo);

    public async Task<bool> VerificarSenhaAsync(int id, string senha)
    {
        var f = await GetByIdAsync(id);
        return f is not null && hash.Verify(f.Senha!, senha);
    }
}