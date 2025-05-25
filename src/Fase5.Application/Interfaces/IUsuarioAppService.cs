using Fase5.Application.Dtos.Login.Request;
using Fase5.Application.Dtos.Login.Response;

namespace Fase5.Application.Interfaces;

public interface IUsuarioAppService
{
    Task<LoginResponse> LoginAsync(LoginRequest dto);
}