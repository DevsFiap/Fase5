using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Services;

public interface IUsuarioDomainService : IBaseDomainService<Usuario, Guid>
{
    Task<Usuario?> ObterPorLoginAsync(string login);
}