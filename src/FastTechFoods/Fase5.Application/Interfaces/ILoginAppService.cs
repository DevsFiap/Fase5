using Fase5.Application.Dtos.Login.Request;
using Fase5.Application.Dtos.Login.Response;

namespace Fase5.Application.Interfaces;

public interface ILoginAppService
{
    Task<LoginResponse> LoginAsync(LoginRequest dto);
}