using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NetMarket.Core.Entities.OrdemCompra;

namespace NetMarket.BusinessLogic.Data.Configuration
{
    public class OrderCompraConfiguration : IEntityTypeConfiguration<OrdemCompras>
    {
        public void Configure(EntityTypeBuilder<OrdemCompras> builder)
        {
            ///Estou informando que não pode ter um endereço de envio sem antes ter uma ordem de compra.
            builder.OwnsOne(o => o.EnderecoEnvio, x =>
            {
                x.WithOwner();
            });

            builder.Property(s => s.Status)
                .HasConversion(
                o => o.ToString(),
                o => (OrdemStatus)Enum.Parse(typeof(OrdemStatus), o)
                );

            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
