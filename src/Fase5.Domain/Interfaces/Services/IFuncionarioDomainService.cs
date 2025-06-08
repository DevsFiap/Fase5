using Fase5.Domain.Core;
using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;

namespace Fase5.Domain.Interfaces.Services;

public interface IFuncionarioDomainService : IBaseDomainService<Funcionario, int>
{
    Task<Funcionario?> ObterPorEmailAsync(string email);
    Task<IEnumerable<Funcionario>> BuscarPorCargoAsync(CargoFuncionario cargo);
    Task<bool> VerificarSenhaAsync(int id, string senha);
}