using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.UnitOfWork;
using ComputerStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("most-popular")]
        public async Task<ApiResult> MostPopularProducts()
        {
            return new ApiResult(Status.Ok, await _unitOfWork.ProductRepository.GetBestSellingProductsAsync());
        }

        [HttpGet("most-expensive")]
        public async Task<ApiResult> MostExpensiveProducts()
        {
            return new ApiResult(Status.Ok, await _unitOfWork.ProductRepository.FindByConditionAsync(null, null, p => p.Price, false));
        }
    }
}
