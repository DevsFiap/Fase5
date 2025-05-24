using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public class UsuarioRepository(DataContext _dataContext) : BaseRepository<Usuario, Guid>(_dataContext), IUsuarioRepository
{
    public async Task<Usuario?> ObterPorLoginAsync(string login)
        => await _dataContext.Set<Usuario>().AsNoTracking().FirstOrDefaultAsync(u => u.Login == login);
}