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

        public virtual async Task<IEnumerable<TEntity>> FindByConditionAsync(
            Expression<Func<TEntity, bool>>? condition = null,
            Expression<Func<TEntity, object>>[]? includes = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            bool isAscending = true,
            int skip = 0,
            int? take = null)
        {
            IQueryable<TEntity> entites = _context.Set<TEntity>();

            if (condition != null)
            {
                entites = entites.Where(condition);
            }
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    entites = entites.Include(include);
                }
            }
            if (orderBy != null)
            {
                if (isAscending)
                {
                    entites = entites.OrderBy(orderBy);
                }
                else
                {
                    entites = entites.OrderByDescending(orderBy);
                }
            }

            entites = entites.Skip(skip);

            if (take != null)
            {
                entites = entites.Take(take.Value);
            }

            return entites;
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
