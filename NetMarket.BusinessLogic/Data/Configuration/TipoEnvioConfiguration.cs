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
    public class TipoEnvioConfiguration : IEntityTypeConfiguration<TipoEnvio>
    {
        public void Configure(EntityTypeBuilder<TipoEnvio> builder)
        {
            
        }
    }
}
