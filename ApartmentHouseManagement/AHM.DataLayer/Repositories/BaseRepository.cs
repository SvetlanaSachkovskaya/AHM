using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AHM.DataLayer.Interfaces;

namespace AHM.DataLayer.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AhmContext Context;
        private readonly DbSet<TEntity> _dbSet;


        public BaseRepository(AhmContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }


        public virtual async Task<TEntity> GetByIdAsync(int key)
        {
            return await _dbSet.FindAsync(key);
        }

        public virtual async Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await GetQuery(filter).ToListAsync();
        }

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                _dbSet.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                Context.Entry(entity).State = EntityState.Unchanged;
                throw;
            }
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    _dbSet.Attach(entity);
                    Context.Entry(entity).State = EntityState.Modified;
                }
            }
            catch (Exception)
            {
                foreach (var entity in entities)
                {
                    Context.Entry(entity).State = EntityState.Unchanged;
                }
                throw;
            }
        }

        public virtual void Delete(int key)
        {
            var entityToDelete = _dbSet.Find(key);
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteRange(IEnumerable<int> keys)
        {
            foreach (var key in keys)
            {
                var entityToDelete = _dbSet.Find(key);
                if (Context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _dbSet.Attach(entityToDelete);
                }
                _dbSet.Remove(entityToDelete);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.AnyAsync();
        }


        protected IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}