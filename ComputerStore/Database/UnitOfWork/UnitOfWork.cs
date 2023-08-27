using ComputerStore.Database.Repositories;

namespace ComputerStore.Database.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ComputerStoreDbContext _context;

        public UnitOfWork(ComputerStoreDbContext context)
        {
            _context = context;
        }

        public IRepositoryBase<TEntity> RepositoryBase<TEntity>() where TEntity : class
        {
            return new RepositoryBase<TEntity>(_context);
        }

        public async Task<bool> Save()
        {
            return Convert.ToBoolean(await _context.SaveChangesAsync());
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}
