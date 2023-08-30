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

        [HttpPost]
        public async Task<ApiResult> AddPurchase([FromBody] PurchaseModel purchaseModel)
        {
            await _purchaseRepository.AddAsync(purchaseModel);
            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Created, purchaseModel);
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpPut("{id}")]
        public async Task<ApiResult> EditPurchase([FromRoute] Guid id, [FromBody] PurchaseModel purchaseModel)
        {
            purchaseModel.Id = id;

            _purchaseRepository.Update(purchaseModel);
            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Ok, purchaseModel);
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult> DeletePurchase([FromRoute] Guid id)
        {
            _purchaseRepository.Delete(id);
            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Ok);
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpGet("{id}")]
        public async Task<ApiResult> PurchaseDetails([FromRoute] Guid id)
        {
            Purchase? purchase = await _purchaseRepository.FindByIdAsync(id);

            if (purchase != null)
            {
                PurchaseModel purchaseModel = purchase;

                return new ApiResult(Status.Ok, purchaseModel);
            }

            return new ApiResult(Status.NotFound);
        }
    }
}
