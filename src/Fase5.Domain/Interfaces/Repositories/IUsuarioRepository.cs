using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Repositories;

public interface IUsuarioRepository : IBaseRepository<Usuario, Guid>
{
    Task<Usuario?> ObterPorLoginAsync(string login);
}