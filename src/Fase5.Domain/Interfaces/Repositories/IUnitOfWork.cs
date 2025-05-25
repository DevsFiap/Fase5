namespace Fase5.Domain.Interfaces.Repositories;

/// <summary>
/// Interface para unidade de trabalho dos repositórios
/// </summary>
public interface IUnitOfWork : IDisposable
{
    #region Gerenciamento de transações

    Task SaveChangesAsync();

    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();

    #endregion

    #region Propriedades para acesso aos repositórios

    IConsultaRepository ConsultaRepository { get; }
    IHorarioDisponivelRepository HorarioDisponivelRepository{ get; }
    IMedicoRepository MedicoRepository { get; }
    IPacienteRepository PacienteRepository { get; }
    IUsuarioRepository UsuarioRepository { get; }

    #endregion
}