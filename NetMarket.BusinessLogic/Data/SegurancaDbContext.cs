using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using NetMarket.Core.Entities;

namespace NetMarket.BusinessLogic.Data
{
    public class SegurancaDbContext: IdentityDbContext<Usuario>
    {
        public SegurancaDbContext(DbContextOptions<SegurancaDbContext> options) : base (options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
