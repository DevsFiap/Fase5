using Fase5.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fase5.Infra.Data.Configuration;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
         .ValueGeneratedOnAdd();

        builder.Property(p => p.CriadoEm)
         .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(p => p.Entrega)
         .HasConversion<int>()
         .IsRequired();

        builder.Property(p => p.Status)
         .HasConversion<int>()
         .IsRequired();

        builder.Property(p => p.JustificativaCancelamento)
         .HasMaxLength(255);

        builder.HasOne(p => p.Cliente)
         .WithMany(c => c.Pedidos)
         .HasForeignKey(p => p.ClienteId);

        builder.HasMany(p => p.Itens)
         .WithOne()
         .HasForeignKey(i => i.PedidoId);

        builder.Ignore(p => p.Total);
    }
}