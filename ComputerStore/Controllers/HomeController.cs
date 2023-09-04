using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("best-selling-products")]
        public async Task<ApiResult> GetBestSellingProducts()
        {
            return new ApiResult(Status.Ok, await _unitOfWork.ProductRepository.GetBestSellingProductsAsync(1));
        }
    }
}
