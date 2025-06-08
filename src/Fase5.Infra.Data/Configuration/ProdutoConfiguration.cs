using Fase5.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fase5.Infra.Data.Configuration;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
         .ValueGeneratedOnAdd();

        builder.Property(p => p.Nome)
         .HasMaxLength(80)
         .IsRequired();

        builder.Property(p => p.Descricao)
         .HasMaxLength(255);

        builder.Property(p => p.Preco)
         .HasColumnType("decimal(10,2)")
         .IsRequired();

        builder.Property(p => p.Disponivel)
         .HasDefaultValue(true);

        builder.Property(p => p.Categoria)
         .HasConversion<int>()
         .IsRequired();

        builder.HasIndex(p => p.Nome);
    }
}