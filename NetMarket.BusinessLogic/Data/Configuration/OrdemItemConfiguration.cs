using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NetMarket.Core.Entities.OrdemCompra;

namespace NetMarket.BusinessLogic.Data.Configuration
{
    public class OrdemItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdenado, x =>
            {
                x.WithOwner();
            });

            builder.Property(o => o.Preco).HasColumnType("decimal(18,2)");
        }
    }
}
