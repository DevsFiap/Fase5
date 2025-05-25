using Fase5.Application.Dtos.Login.Request;
using Fase5.Application.Dtos.Login.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Domain.Helpers;
using Fase5.Domain.Interfaces.Security;
using Fase5.Domain.Interfaces.Services;
using Fase5.Infra.Security.Settings;
using Microsoft.Extensions.Options;

namespace Fase5.Application.Services;

public class UsuarioAppService(
        IUsuarioDomainService _usuarioSrv,
        IAuthorizationSecurity _tokenSvc,
        IOptions<JwtSettings> _jwtOpts) : IUsuarioAppService
{
    private readonly JwtSettings _jwt = _jwtOpts.Value;

    public async Task<LoginResponse> LoginAsync(LoginRequest dto)
    {
        Usuario? usuario;

        if (DocumentoHelper.CpfValido(dto.Login))
            usuario = await _usuarioSrv.ObterPorCpfAsync(dto.Login);
        else if (DocumentoHelper.CrmValido(dto.Login))
            usuario = await _usuarioSrv.ObterPorCrmAsync(dto.Login);
        else
            throw new UnauthorizedAccessException("Formato de login inválido.");

        if (usuario is null ||
            !await _usuarioSrv.VerificarSenhaAsync(usuario, dto.Senha))
            throw new UnauthorizedAccessException("Credenciais inválidas.");

        return GerarResponse(usuario);
    }

    private LoginResponse GerarResponse(Usuario usuario)
    {
        var token = _tokenSvc.CreateToken(usuario);

        var role = usuario.Perfil == PerfilUsuario.Medico ? "medico" : "user";

        return new LoginResponse(
            usuario.Id,
            usuario.Nome!,
            role,
            token,
            DateTime.UtcNow.AddHours(_jwt.ExpirationInHours));
    }
}