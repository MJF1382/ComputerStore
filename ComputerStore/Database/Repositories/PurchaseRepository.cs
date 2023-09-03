using ComputerStore.Database.Entities;
using ComputerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Database.Repositories
{
    public class PurchaseRepository : RepositoryBase<Purchase, ComputerStoreDbContext>, IPurchaseRepository
    {
        public PurchaseRepository(ComputerStoreDbContext context) : base(context)
        {
        }

        public async Task<List<PurchaseModel>> GetAllPurchasesAsync()
        {
            List<PurchaseModel> purchaseModels = new List<PurchaseModel>();
            List<Purchase> purchases = await _context.Purchases.Include(p => p.ProductPurchases).ThenInclude(p => p.Product).ToListAsync();

            foreach (Purchase purchase in purchases)
            {
                PurchaseModel purchaseModel = purchase;
                purchaseModel.ProductIds = purchase.ProductPurchases.Select(p => p.Product.Id).ToList();

                purchaseModels.Add(purchaseModel);
            }

            return purchaseModels;
        }
    }
}
