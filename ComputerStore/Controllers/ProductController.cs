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
            List<ProductModel> products = (await _productRepository.GetAllAsync()).Select<Product, ProductModel>(product => product).ToList();

            return new ApiResult(Status.Ok, products);
        }

        [HttpPost]
        public async Task<ApiResult> AddProduct([FromBody] ProductModel productModel)
        {
            await _productRepository.AddAsync(productModel);
            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Created, productModel);
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpPut("{id}")]
        public async Task<ApiResult> EditProduct([FromRoute] Guid id, [FromBody] ProductModel productModel)
        {
            productModel.Id = id;

            _productRepository.Update(productModel);
            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Ok, productModel);
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult> DeleteProduct([FromRoute] Guid id)
        {
            _productRepository.Delete(id);
            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Ok);
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpGet("{id}")]
        public async Task<ApiResult> ProductDetails([FromRoute] Guid id)
        {
            Product? product = await _productRepository.FindByIdAsync(id);

            if (product != null)
            {
                ProductModel productModel = product;

                return new ApiResult(Status.Ok, productModel);
            }

            return new ApiResult(Status.NotFound);
        }
    }
}
