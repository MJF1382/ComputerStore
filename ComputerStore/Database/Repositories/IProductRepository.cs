using ComputerStore.Database.Entities;
using ComputerStore.Models;

namespace ComputerStore.Database.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<List<ProductModel>> GetBestSellingProductsAsync(int? productCount = null);
    }
}
