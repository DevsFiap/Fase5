using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public class UsuarioRepository(DataContext ctx) : BaseRepository<Usuario, Guid>(ctx), IUsuarioRepository
{
    public Task<Usuario?> ObterPorLoginAsync(string login)
        => ctx.Set<Usuario>().FirstOrDefaultAsync(u => u.Login == login);
}