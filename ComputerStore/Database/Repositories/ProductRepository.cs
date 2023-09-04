using ComputerStore.Database.Entities;
using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Database.Repositories
{
    public class ProductRepository : RepositoryBase<Product, ComputerStoreDbContext>, IProductRepository
    {
        public ProductRepository(ComputerStoreDbContext context) : base(context)
        {

        }

        public async Task<List<ProductModel>> GetBestSellingProductsAsync(int? productCount = null)
        {
            var bestSellingProductPurchases = await (from product in _context.Products
                                                     join productPurchase in _context.ProductPurchases on product.Id equals productPurchase.ProductId
                                                     group productPurchase by productPurchase.ProductId into groupedProductPurchases
                                                     orderby groupedProductPurchases.Sum(p => p.Quantity) descending
                                                     select new
                                                     {
                                                         Id = groupedProductPurchases.Key,
                                                         Quantity = groupedProductPurchases.Sum(p => p.Quantity),
                                                         Products = groupedProductPurchases.AsQueryable().Include(p => p.Product).ToList()
                                                     }).Select(p => p.Products).ToListAsync();

            List<ProductModel> bestSellingProduct = new List<ProductModel>();

            if (productCount.HasValue)
            {
                bestSellingProduct = bestSellingProductPurchases.SelectMany(productPurchases => productPurchases).Select(productPurchase => productPurchase.Product).DistinctBy(p => p.Id).ToList().Select<Product, ProductModel>(product => product).Take(productCount.Value).ToList();
            }
            else
            {
                bestSellingProduct = bestSellingProductPurchases.SelectMany(productPurchases => productPurchases).Select(productPurchase => productPurchase.Product).DistinctBy(p => p.Id).ToList().Select<Product, ProductModel>(product => product).ToList();
            }

            return bestSellingProduct;
        }
    }
}
