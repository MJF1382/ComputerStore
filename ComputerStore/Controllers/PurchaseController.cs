using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.Repositories;
using ComputerStore.Database.UnitOfWork;
using ComputerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/purchases")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Purchase> _purchaseRepository;

        public PurchaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _purchaseRepository = _unitOfWork.RepositoryBase<Purchase>();
        }

        [HttpGet]
        public async Task<ApiResult> GetPurchases()
        {
            List<PurchaseModel> purchases = (await _purchaseRepository.GetAllAsync()).Select<Purchase, PurchaseModel>(purchase => purchase).ToList();

            return new ApiResult(Status.Ok, purchases);
        }
    }
}
