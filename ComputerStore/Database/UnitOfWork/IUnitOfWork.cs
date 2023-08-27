using ComputerStore.Database.Repositories;

namespace ComputerStore.Database.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepositoryBase<TEntity> RepositoryBase<TEntity>() where TEntity : class;
        Task<bool> Save();
    }
}
