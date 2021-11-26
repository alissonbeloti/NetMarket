using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NetMarket.Core.Entities;

namespace NetMarket.BusinessLogic.Data.Configuration
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).IsRequired().HasMaxLength(250).HasColumnType("varchar");
            builder.Property(p => p.Descricao).IsRequired().HasMaxLength(2000).HasColumnType("varchar");
            builder.Property(p => p.Imagem).HasMaxLength(1000).HasColumnType("varchar");
            builder.Property(p => p.Preco).HasColumnType("decimal").HasPrecision(18, 4).IsRequired();
            builder.HasOne(m => m.Marca).WithMany().HasForeignKey(p => p.MarcaId);
            builder.HasOne(c => c.Categoria).WithMany().HasForeignKey(p => p.CategoriaId);
        }
    }
}
