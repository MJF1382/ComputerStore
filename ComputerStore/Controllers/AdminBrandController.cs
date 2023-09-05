using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.Repositories;
using ComputerStore.Database.UnitOfWork;
using ComputerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class AdminBrandController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Brand> _brandRepository;

        public AdminBrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = _unitOfWork.RepositoryBase<Brand>();
        }

        [HttpGet]
        public async Task<ApiResult> GetBrands()
        {
            List<BrandModel> brands = (await _brandRepository.GetAllAsync()).Select<Brand, BrandModel>(brand => brand).ToList();

            return new ApiResult(Status.Ok, brands);
        }

        [HttpPost]
        public async Task<ApiResult> AddBrand([FromBody] BrandModel brandModel)
        {
            brandModel.Id = Guid.NewGuid();

            await _brandRepository.AddAsync(brandModel);
            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Created, brandModel);
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpPut("{id}")]
        public async Task<ApiResult> EditBrand([FromRoute] Guid id, [FromBody] BrandModel brandModel)
        {
            brandModel.Id = id;

            _brandRepository.Update(brandModel);
            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Ok, brandModel);
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult> DeleteBrand([FromRoute] Guid id)
        {
            _brandRepository.Delete(id);
            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Ok);
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpGet("{id}")]
        public async Task<ApiResult> BrandDetails([FromRoute] Guid id)
        {
            Brand? brand = await _brandRepository.FindByIdAsync(id);

            if (brand != null)
            {
                BrandModel brandModel = brand;

                return new ApiResult(Status.Ok, brandModel);
            }

            return new ApiResult(Status.NotFound);
        }
    }
}
