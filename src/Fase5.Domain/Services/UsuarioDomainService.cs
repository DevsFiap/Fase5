using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Domain.Services;

public sealed class UsuarioDomainService(IUnitOfWork _unitOfWork) 
    : BaseDomainService<Usuario, Guid>(_unitOfWork.UsuarioRepository), IUsuarioDomainService
{
    public Task<Usuario?> ObterPorLoginAsync(string login)
        => _unitOfWork.UsuarioRepository.ObterPorLoginAsync(login);
}