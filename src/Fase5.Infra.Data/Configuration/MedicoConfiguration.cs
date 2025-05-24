using Fase5.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fase5.Infra.Data.Configuration;

public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
{
    public void Configure(EntityTypeBuilder<Medico> builder)
    {
        builder.ToTable("Medicos");
        builder.HasBaseType<Usuario>();

        builder.Property(m => m.CRM)
               .IsRequired()
               .HasMaxLength(12);

        builder.HasIndex(m => m.CRM)
               .IsUnique();

        builder.Property(m => m.Especialidade)
               .IsRequired()
               .HasMaxLength(60);

        builder.Property(m => m.ValorConsultaPadrao)
               .HasColumnType("decimal(10,2)");
    }
}