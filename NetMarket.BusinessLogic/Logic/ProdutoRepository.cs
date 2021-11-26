using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using NetMarket.BusinessLogic.Data;
using NetMarket.Core.Entities;
using NetMarket.Core.Interfaces;

namespace NetMarket.BusinessLogic.Logic
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly MarketDbContext context;

        public ProdutoRepository(MarketDbContext context)
        {
            this.context = context;
        }
        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            return await this.context.Produtos
                .Include(p => p.Marca)
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Produto>> GetProdutosAsync()
        {
            return await this.context.Produtos
                .Include(p => p.Marca)
                .Include(p => p.Categoria)
                .ToListAsync();
        }
    }
}
