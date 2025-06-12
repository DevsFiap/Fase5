using Fase5.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fase5.Infra.Data.Configuration;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
         .ValueGeneratedOnAdd();

        builder.Property(c => c.Nome)
         .HasMaxLength(80)
         .IsRequired();

        builder.Property(c => c.Email)
         .HasMaxLength(80)
         .IsRequired();
        builder.HasIndex(c => c.Email).IsUnique();

        builder.Property(c => c.Senha)
         .HasMaxLength(128)
         .IsRequired();

        builder.Property(c => c.CPF)
         .HasColumnType("char(11)")
         .IsRequired();
        builder.HasIndex(c => c.CPF).IsUnique();
    }
}