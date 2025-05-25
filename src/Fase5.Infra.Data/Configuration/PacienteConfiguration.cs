using Fase5.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fase5.Infra.Data.Configuration;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Pacientes");
        builder.HasBaseType<Login>();

        builder.Property(p => p.CPF)
               .IsRequired()
               .HasMaxLength(11);

        builder.HasIndex(p => p.CPF)
               .IsUnique();
    }
}