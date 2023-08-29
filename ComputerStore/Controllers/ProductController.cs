using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.Repositories;
using ComputerStore.Database.UnitOfWork;
using ComputerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Product> _productRepository;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productRepository = _unitOfWork.RepositoryBase<Product>();
        }

        [HttpGet]
        public async Task<ApiResult> GetProducts()
        {
            List<ProductModel> products = (await _productRepository.GetAllAsync()).Select(product => new ProductModel()
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                Title = product.Title,
                Summary = product.Summary,
                Warranty = product.Warranty,
                Price = product.Price,
                OfferedPrice = product.OfferedPrice,
                Quantity = product.Quantity
            }).ToList();

            return new ApiResult(Status.Ok, products);
        }
    }
}
