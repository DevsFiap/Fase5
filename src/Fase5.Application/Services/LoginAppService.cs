using AutoMapper;
using Fase5.Application.Dtos.Login.Request;
using Fase5.Application.Dtos.Login.Response;
using Fase5.Application.Interfaces;
using Fase5.Domain.Enuns;
using Fase5.Domain.Interfaces.Security;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Application.Services;

public class LoginAppService(
        IAuthDomainService _auth,
        ITokenService _token,
        IMapper _map) : ILoginAppService
{
    public async Task<LoginResponse> LoginAsync(LoginRequest dto)
    {
        // tenta cliente
        var cliente = await _auth.AutenticarClienteAsync(dto.Login, dto.Senha);
        if (cliente is not null)
        {
            var jwt = _token.CreateToken(cliente.Id, cliente.Email, "cliente");
            return _map.Map<LoginResponse>((cliente.Id, cliente.Nome, "cliente", jwt));
        }

        // tenta funcionário
        var func = await _auth.AutenticarFuncionarioAsync(dto.Login, dto.Senha)
                   ?? throw new UnauthorizedAccessException("Credenciais inválidas.");

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
