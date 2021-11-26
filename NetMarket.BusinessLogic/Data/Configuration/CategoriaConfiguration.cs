using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NetMarket.Core.Entities;

namespace NetMarket.BusinessLogic.Data.Configuration
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Nome).HasColumnType("varchar").HasMaxLength(100).IsRequired();
        }
    }
}
