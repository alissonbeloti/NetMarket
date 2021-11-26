using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

using NetMarket.BusinessLogic.Data;
using NetMarket.Core.Entities;
using NetMarket.Core.Interfaces;
using NetMarket.Core.Specification;

namespace NetMarket.BusinessLogic.Logic
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity: BaseEntity
    {
        private readonly MarketDbContext context;

        public GenericRepository(MarketDbContext context)
        {
            this.context = context;
        }
        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(IEspecification<TEntity> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TEntity> GetByIdWithSpecAsync(IEspecification<TEntity> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecification(IEspecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(context.Set<TEntity>().AsQueryable(), spec);
        }

        public async Task<int> CountAsync(IEspecification<TEntity> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<int> Add(TEntity entity)
        {
            context.Add(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Update(TEntity entity)
        {
            context.Update(entity);
            return await context.SaveChangesAsync();
        }
    }
}
