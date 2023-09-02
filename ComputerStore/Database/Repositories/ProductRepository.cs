using ComputerStore.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Database.Repositories
{
    public class ProductRepository : RepositoryBase<Product, ComputerStoreDbContext>, IProductRepository
    {
        public ProductRepository(ComputerStoreDbContext context) : base(context)
        {

        }

        //public async Task<Product> BestSellingProducts()
        //{
        //    _context.ProductPurchases.
        //}
    }
}
