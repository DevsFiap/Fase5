using AutoMapper;
using Fase5.Application.Dtos.Login.Request;
using Fase5.Application.Dtos.Login.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Enuns;
using Fase5.Domain.Helpers;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Security;

namespace Fase5.Application.Services;

public class LoginAppService(
        IUnitOfWork _uow,
        IPasswordHashService _hash,
        ITokenService _token,
        IMapper _map) : ILoginAppService
{
    public async Task<LoginResponse> LoginAsync(LoginRequest dto)
    {
        /* -------- tenta Cliente (CPF ou e-mail) -------- */
        var cliente = DocumentoHelper.CpfValido(dto.Login)
            ? await _uow.ClienteRepository.ObterPorCpfAsync(dto.Login)
            : await _uow.ClienteRepository.ObterPorEmailAsync(dto.Login);

        if (cliente is not null && _hash.Verify(cliente.Senha!, dto.Senha))
        {
            var jwt = _token.CreateToken(cliente.Id, cliente.Email, "cliente");
            return _map.Map<LoginResponse>((cliente.Id, cliente.Nome, "cliente", jwt));
        }

        /* -------- tenta Funcionário (e-mail corporativo) -------- */
        var func = await _uow.FuncionarioRepository.ObterPorEmailAsync(dto.Login)
                   ?? throw new UnauthorizedAccessException("Credenciais inválidas.");

        if (!_hash.Verify(func.Senha!, dto.Senha))
            throw new UnauthorizedAccessException("Credenciais inválidas.");

        var role = func.Cargo switch
        {
            CargoFuncionario.Gerente => "gerente",
            CargoFuncionario.Cozinha => "cozinha",
            _ => "atendente"
        };

        var token = _token.CreateToken(func.Id, func.Email, role);
        return _map.Map<LoginResponse>((func.Id, func.Nome, role, token));
    }
}