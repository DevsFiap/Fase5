using Fase5.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fase5.Infra.Data.Configuration;

public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
{
    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    {
        builder.ToTable("ItensPedido");

        builder.HasKey(i => new { i.PedidoId, i.ProdutoId });

        builder.Property(i => i.Quantidade)
         .IsRequired();

        builder.Property(i => i.PrecoUnitario)
         .HasColumnType("decimal(10,2)")
         .IsRequired();

        builder.HasOne(i => i.Produto)
         .WithMany()
         .HasForeignKey(i => i.ProdutoId);
    }
}