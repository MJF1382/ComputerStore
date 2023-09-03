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
        private readonly IRepositoryBase<ProductPurchase> _productPurchaseRepository;

        public PurchaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productPurchaseRepository = _unitOfWork.RepositoryBase<ProductPurchase>();
        }

        [HttpGet]
        public async Task<ApiResult> GetPurchases()
        {
            return new ApiResult(Status.Ok, await _unitOfWork.PurchaseRepository.GetAllPurchasesAsync());
        }
    }
}
