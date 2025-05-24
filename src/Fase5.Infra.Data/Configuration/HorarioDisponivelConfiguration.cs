using Fase5.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fase5.Infra.Data.Configuration;

public class HorarioDisponivelConfiguration : IEntityTypeConfiguration<HorarioDisponivel>
{
    public void Configure(EntityTypeBuilder<HorarioDisponivel> builder)
    {
        builder.ToTable("HorariosDisponiveis");

        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id)
               .ValueGeneratedOnAdd();

        builder.Property(h => h.Inicio).IsRequired();
        builder.Property(h => h.Fim).IsRequired();
        builder.Property(h => h.Ocupado).IsRequired();

        builder.HasOne(h => h.Medico)
               .WithMany(m => m.HorariosDisponiveis)
               .HasForeignKey(h => h.MedicoId);

        // Evita horários sobrepostos (mesmo médico)
        builder.HasIndex(h => new { h.MedicoId, h.Inicio, h.Fim })
               .IsUnique();
    }
}