using Fase5.Domain.Enuns;

namespace Fase5.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }
    public string? Nome { get; set; } 
    public string? Email { get; set; }
    public string? Senha { get; set; } 
    public string? CPF { get; set; }

    public PerfilUsuario Perfil { get; private set; } = PerfilUsuario.Cliente;
    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}