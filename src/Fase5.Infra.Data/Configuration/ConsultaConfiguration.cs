using Fase5.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fase5.Infra.Data.Configuration;

public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
{
    public void Configure(EntityTypeBuilder<Consulta> builder)
    {
        builder.ToTable("Consultas");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
               .ValueGeneratedOnAdd();

        builder.Property(c => c.DataHora)
               .IsRequired();

        builder.Property(c => c.Valor)
               .HasColumnType("decimal(10,2)");

        builder.Property(c => c.Status)
               .IsRequired()
               .HasConversion<int>();

        builder.Property(c => c.JustificativaCancelamento)
               .HasMaxLength(255);


        builder.HasOne(c => c.Medico)
               .WithMany(m => m.Consultas)
               .HasForeignKey(c => c.MedicoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Paciente)
               .WithMany(p => p.Consultas)
               .HasForeignKey(c => c.PacienteId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}