using System.Linq.Expressions;

namespace ComputerStore.Database.Repositories
{
    public interface IRepositoryBase<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> FindByIdAsync(object id);
        Task<IEnumerable<TEntity>> FindByConditionAsync(
            Func<TEntity, bool>? condition = null,
            Expression<Func<TEntity, object>>? include = null,
            Func<TEntity, object>? orderBy = null,
            bool isAscending = true,
            int skip = 0,
            int? take = null);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
    }
}
