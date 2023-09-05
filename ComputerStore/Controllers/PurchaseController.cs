using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.Repositories;
using ComputerStore.Database.UnitOfWork;
using ComputerStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

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
            if (purchaseModel.ProductPurchases.Count > 0)
            {
                purchaseModel.Id = Guid.NewGuid();

                await _unitOfWork.PurchaseRepository.AddAsync(purchaseModel);
                var productPurchases = purchaseModel.ProductPurchases
                    .Select<ProductPurchaseModel, ProductPurchase>(productPurchase => productPurchase)
                    .ToList();
                productPurchases.ForEach(productPurchase => productPurchase.PurchaseId = purchaseModel.Id);
                await _productPurchaseRepository.AddRangeAsync(productPurchases);

                bool result = await _unitOfWork.Save();

                if (result)
                {
                    return new ApiResult(Status.Created, purchaseModel);
                }

                return new ApiResult(Status.InternalServerError);
            }

            return new ApiResult(Status.BadRequest, null, new List<string>() { "هیچ محصولی در سبد خرید شما وجود ندارد." });
        }

        [HttpPut("{id}")]
        public async Task<ApiResult> EditPurchase([FromRoute] Guid id, [FromBody] PurchaseModel purchaseModel)
        {
            purchaseModel.Id = id;

            _unitOfWork.PurchaseRepository.Update(purchaseModel);
            await _unitOfWork.PurchaseRepository.RemoveAllProductsFromPurchase(purchaseModel.Id);
            var productPurchases = purchaseModel.ProductPurchases
                    .Select<ProductPurchaseModel, ProductPurchase>(productPurchase => productPurchase)
                    .ToList();
            productPurchases.ForEach(productPurchase => productPurchase.PurchaseId = purchaseModel.Id);
            await _productPurchaseRepository.AddRangeAsync(productPurchases);

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
            _unitOfWork.PurchaseRepository.Delete(id);
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
            Purchase? purchase = (await _unitOfWork.PurchaseRepository.FindByConditionAsync(p => p.Id == id, new Expression<Func<Purchase, object>>[] { p => p.ProductPurchases })).FirstOrDefault();

            if (purchase != null)
            {
                PurchaseModel purchaseModel = purchase;
                
                return new ApiResult(Status.Ok, purchaseModel);
            }

            return new ApiResult(Status.NotFound);
        }
    }
}
