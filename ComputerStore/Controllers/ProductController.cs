using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.UnitOfWork;
using ComputerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<ApiResult> GetProductDetails([FromRoute] Guid id)
        {
            Product? product = (await _unitOfWork.ProductRepository.FindByConditionAsync(
                p => p.Id == id,
                new System.Linq.Expressions.Expression<Func<Product, object>>[] {
                    p => p.Brand,
                    p => p.Category,
                    p => p.Comments,
                    p => p.ExtraDetails
                }))
                .FirstOrDefault();

            if (product != null)
            {
                ProductModel productModel = product;

                return new ApiResult(Status.Ok, productModel);
            }

            return new ApiResult(Status.NotFound);
        }
    }
}
