using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetMarket.Core.Entities;
using NetMarket.Core.Specification;

namespace NetMarket.Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<IReadOnlyList<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdWithSpecAsync(IEspecification<TEntity> spec);

        Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(IEspecification<TEntity> spec);

        Task<int> CountAsync(IEspecification<TEntity> spec);

        Task<int> Add(TEntity entity);

        Task<int> Update(TEntity entity);
    }
}
