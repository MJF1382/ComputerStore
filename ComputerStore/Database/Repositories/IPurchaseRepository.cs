using ComputerStore.Database.Entities;
using ComputerStore.Models;

namespace ComputerStore.Database.Repositories
{
    public interface IPurchaseRepository : IRepositoryBase<Purchase>
    {
        Task<List<PurchaseModel>> GetAllPurchasesAsync();
    }
}
