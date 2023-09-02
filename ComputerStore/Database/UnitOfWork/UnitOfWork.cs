using ComputerStore.Database.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ComputerStore.Database.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ComputerStoreDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ComputerStoreDbContext context)
        {
            _context = context;
        }

        public IRepositoryBase<TEntity> RepositoryBase<TEntity>() where TEntity : class
        {
            return new RepositoryBase<TEntity, ComputerStoreDbContext>(_context);
        }
        public IProductRepository ProductRepository => new ProductRepository(_context);

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
        public void CommitTransaction(out string errorMessage)
        {
            try
            {
                _transaction?.Commit();
                errorMessage = "";
            }
            catch
            {
                RollBackAsync();
                errorMessage = "خطا در اتصال به سرور، لطفا بعدا دوباره امتحان کنید.";
            }
        }
        public async Task RollBackAsync()
        {
            await _transaction?.RollbackAsync();
            _transaction = null;
        }
        public async Task<bool> Save()
        {
            return Convert.ToBoolean(await _context.SaveChangesAsync());
        }
        public ValueTask DisposeAsync()
        {
            _transaction?.DisposeAsync();
            return _context.DisposeAsync();
        }
    }
}
