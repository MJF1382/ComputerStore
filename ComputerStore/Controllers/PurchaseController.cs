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

        [HttpPost]
        public async Task<ApiResult> AddPurchase([FromBody] PurchaseModel purchaseModel)
        {
            if (purchaseModel.ProductIds.Count > 0)
            {
                purchaseModel.Id = Guid.NewGuid();

                await _unitOfWork.PurchaseRepository.AddAsync(purchaseModel);

                foreach (Guid productId in purchaseModel.ProductIds)
                {
                    await _productPurchaseRepository.AddAsync(new ProductPurchase()
                    {
                        ProductId = productId,
                        PurchaseId = purchaseModel.Id
                    });
                }

                bool result = await _unitOfWork.Save();

                if (result)
                {
                    return new ApiResult(Status.Created, purchaseModel);
                }

                return new ApiResult(Status.InternalServerError);
            }

            return new ApiResult(Status.BadRequest, null, new List<string>() { "هیچ محصولی در سبد خرید شما وجود ندارد." });
        }
    }
}
