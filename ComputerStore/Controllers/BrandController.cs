﻿using ComputerStore.Classes;
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

        [HttpPut("{id}")]
        public async Task<ApiResult> EditBrand([FromRoute] Guid id, [FromBody] BrandModel brandModel)
        {
            Brand brand = new Brand()
            {
                Id = id,
                Name = brandModel.Name
            };

            _brandRepository.Update(brand);

            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Ok, new BrandModel() { Id = id, Name = brandModel.Name });
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
                BrandModel brandModel = new BrandModel()
                {
                    Id = brand.Id,
                    Name = brand.Name
                };

                return new ApiResult(Status.Ok, brandModel);
            }

            return new ApiResult(Status.NotFound);
        }
    }
}
