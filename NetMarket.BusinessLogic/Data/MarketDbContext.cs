using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using NetMarket.BusinessLogic.Data.Configuration;
using NetMarket.Core.Entities;

namespace NetMarket.BusinessLogic.Data
{
    public class MarketDbContext: DbContext
    {
        public MarketDbContext(DbContextOptions<MarketDbContext> options): base(options) { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Produto>(new ProdutoConfiguration().Configure);
            //builder.Entity<Categoria>(new CategoriaConfiguration().Configure);
            //builder.Entity<Marca>(new MarcaConfiguration().Configure);
            //Essa linha substituí todas acima.
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
