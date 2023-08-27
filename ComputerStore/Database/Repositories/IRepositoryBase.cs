using System.Linq.Expressions;

namespace ComputerStore.Database.Repositories
{
    public interface IRepositoryBase<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> FindByIdAsync(object id);
        Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> condition);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
    }
}
