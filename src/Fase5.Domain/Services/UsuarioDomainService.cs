using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Fase5.Domain.Services;

public sealed class UsuarioDomainService(IUnitOfWork uow)
    : BaseDomainService<Usuario, Guid>(uow.UsuarioRepository), IUsuarioDomainService
{
    private readonly PasswordHasher<Usuario> _hasher = new();

    public async Task<Usuario?> ObterPorCpfAsync(string cpf)
        => await uow.PacienteRepository.ObterPorCpfAsync(cpf);

    public async Task<Usuario?> ObterPorCrmAsync(string crm)
        => await uow.MedicoRepository.ObterPorCrmAsync(crm);

    public Task<bool> VerificarSenhaAsync(Usuario usuario, string senha)
    {
        var result = _hasher.VerifyHashedPassword(usuario, usuario.Senha!, senha);
        return Task.FromResult(result == PasswordVerificationResult.Success);
    }
}