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

        public override void Delete(Purchase entity)
        {
            base.Delete(entity);

            List<ProductPurchase> productPurchases = _context.ProductPurchases.Where(p => p.PurchaseId == entity.Id).ToList();
            _context.ProductPurchases.RemoveRange(productPurchases);
            _context.Purchases.Remove(entity);
        }

        public override void Delete(object id)
        {
            base.Delete(id);

            Purchase? purchase = FindByIdAsync(id).Result;

            if (purchase != null)
            {
                List<ProductPurchase> productPurchases = _context.ProductPurchases.Where(p => p.PurchaseId == purchase.Id).ToList();
                _context.ProductPurchases.RemoveRange(productPurchases);
                _context.Purchases.Remove(purchase);
            }
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

        public async Task RemoveAllProductsFromPurchase(Guid id)
        {
            Purchase? purchaseFind = _context.Purchases.Include(p => p.ProductPurchases).ToList().Find(p => p.Id == id);

            if (purchaseFind != null)
            {
                _context.ProductPurchases.RemoveRange(purchaseFind.ProductPurchases);
            }

            await Task.CompletedTask;
        }
    }
}
