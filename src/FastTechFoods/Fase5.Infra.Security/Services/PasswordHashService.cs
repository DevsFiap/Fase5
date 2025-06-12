using Fase5.Domain.Interfaces.Security;
using Microsoft.AspNetCore.Identity;

namespace Fase5.Infra.Security.Services;

public sealed class PasswordHashService : IPasswordHashService
{
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string plainTextPassword)
        => _hasher.HashPassword(null!, plainTextPassword);

    public bool Verify(string hash, string plainTextPassword)
        => _hasher.VerifyHashedPassword(null!, hash, plainTextPassword)
           == PasswordVerificationResult.Success;
}