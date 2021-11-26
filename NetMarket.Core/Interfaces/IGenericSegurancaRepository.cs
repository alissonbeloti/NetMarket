using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using NetMarket.Core.Specification;

namespace NetMarket.Core.Interfaces
{
    public interface IGenericSegurancaRepository<TEntity> where TEntity : IdentityUser
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
