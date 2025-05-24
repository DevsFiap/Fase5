using Fase5.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fase5.Infra.Data.Configuration;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
               .ValueGeneratedOnAdd();

        builder.Property(u => u.Nome)
               .IsRequired()
               .HasMaxLength(80);

        builder.Property(u => u.Senha)
               .IsRequired();

        builder.Property(u => u.Perfil)
               .IsRequired()
               .HasConversion<int>();
    }
}