using Fase5.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fase5.Infra.Data.Configuration;

public class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.ToTable("Funcionarios");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id)
         .ValueGeneratedOnAdd();

        builder.Property(f => f.Nome)
         .HasMaxLength(80)
         .IsRequired();

        builder.Property(f => f.Email)
         .HasMaxLength(80)
         .IsRequired();
        builder.HasIndex(f => f.Email).IsUnique();

        builder.Property(f => f.Senha)
         .HasMaxLength(128)
         .IsRequired();

        builder.Property(f => f.Cargo)
         .HasConversion<int>()
         .IsRequired();
    }
}