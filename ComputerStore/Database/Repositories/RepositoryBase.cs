using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ComputerStore.Database.Repositories
{
    public class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity> where TEntity : class where TContext : DbContext
    {
        protected readonly TContext _context;

        public RepositoryBase(TContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity?> FindByIdAsync(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>>? include = null)
        {
            if (include != null)
            {
                return await _context.Set<TEntity>().Where(condition).Include(include).ToListAsync();
            }

            return await _context.Set<TEntity>().Where(condition).ToListAsync();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(List<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity? entity = FindByIdAsync(id).Result;

            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
            }
        }
    }
}
