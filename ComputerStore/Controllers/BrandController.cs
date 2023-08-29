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
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Brand> _brandRepository;

        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = _unitOfWork.RepositoryBase<Brand>();
        }

        [HttpGet]
        public async Task<ApiResult> GetBrands()
        {
            List<BrandModel> brands = (await _brandRepository.GetAllAsync()).Select(brand => new BrandModel()
            {
                Id = brand.Id,
                Name = brand.Name
            }).ToList();

            return new ApiResult(Status.Ok, brands);
        }

        [HttpPost]
        public async Task<ApiResult> AddBrand([FromBody] BrandModel brandModel)
        {
            await _brandRepository.AddAsync(new Brand()
            {
                Id = brandModel.Id,
                Name = brandModel.Name
            });

            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Created, brandModel);
            }

            return new ApiResult(Status.InternalServerError);
        }
    }
}
