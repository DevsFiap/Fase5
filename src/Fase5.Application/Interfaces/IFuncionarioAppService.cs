using Fase5.Application.Dtos.Funcionario.Request;
using Fase5.Application.Dtos.Funcionario.Response;
using Fase5.Domain.Enuns;

namespace Fase5.Application.Interfaces;

public interface IFuncionarioAppService
{
    Task<int> CriarAsync(CreateFuncionarioRequest dto);
    Task<FuncionarioResponse?> GetByIdAsync(int id);
    Task<IEnumerable<FuncionarioResponse>> GetAllAsync();
    Task AtualizarAsync(int id, UpdateFuncionarioRequest dto);
    Task DeletarAsync(int id);

    Task<IEnumerable<FuncionarioResponse>> BuscarPorCargoAsync(CargoFuncionario cargo);
}