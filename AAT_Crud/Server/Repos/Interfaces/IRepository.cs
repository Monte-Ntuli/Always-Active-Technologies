using System.Linq.Expressions;

namespace AAT_Crud.Repos.Interfaces
{
    public interface IRepository
    {
        public interface IRepository<TEntity> where TEntity : class
        {
            Task AddAsync(TEntity entity);
            Task AddRangeAsync(IEnumerable<TEntity> entities);

            //Task BulkAddRangeAsync(IEnumerable<TEntity> entities);

            //Task UpsertAsync(TEntity entity);
            //Task UpsertRangeAsync(List<TEntity> entities);

            TEntity Update(TEntity entity);
            void UpdateRange(IEnumerable<TEntity> entities);

            void Remove(TEntity entity);
            void RemoveRange(IEnumerable<TEntity> entities);

            Task<int> CountAsync();
            Task<int> CountAsync(params object[] parameters);

            Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

            Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
            TEntity Get(int id);
            TEntity Get(params object[] parameters);
            Task<TEntity> GetAsync(Guid id);
            Task<TEntity> GetAsync(params object[] parameters);
            Task<ICollection<TEntity>> GetAllAsync();
            Task<ICollection<TEntity>> GetAllAsync(params object[] parameters);
            Task<ICollection<TEntity>> GetAllFromAsync(DateTime date);
        }
    }
}
