using static AAT_Crud.Repos.Interfaces.IRepository;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AAT_Crud.Repos
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;
        public Repository(DbContext context)
        {
            if (context != null)
            {
                _context = context;
                _entities = context.Set<TEntity>();
            }
        }

        public async virtual Task AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return _entities.AddRangeAsync(entities);
        }

        public virtual TEntity Update(TEntity entity)
        {
            _entities.Update(entity);
            return entity;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }


        public virtual Task<int> CountAsync()
        {
            return _entities.CountAsync();
        }

        public virtual async Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public virtual Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefaultAsync(predicate);
        }

        public async virtual Task<TEntity> GetAsync(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public virtual TEntity Get(int id)
        {
            return _entities.Find(id);
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }


        public virtual Task<int> CountAsync(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> GetAsync(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Get(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ICollection<TEntity>> GetAllAsync(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ICollection<TEntity>> GetAllFromAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

    }
}
