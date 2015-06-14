using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AHM.DataLayer.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(int key);

        Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> expression);

        Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);


        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);


        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        void Delete(int key);

        void DeleteRange(IEnumerable<int> keys);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);

        void AutoDetectChanges(bool enable);
    }
}