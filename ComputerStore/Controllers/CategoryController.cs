﻿using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.Repositories;
using ComputerStore.Database.UnitOfWork;
using ComputerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Category> _categoryRepository;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = _unitOfWork.RepositoryBase<Category>();
        }

        [HttpGet]
        public async Task<ApiResult> GetCategories()
        {
            List<CategoryModel> categories = (await _categoryRepository.GetAllAsync()).Select(category => new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();

            return new ApiResult(Status.Ok, categories);
        }

        [HttpPost]
        public async Task<ApiResult> AddCategory([FromBody] CategoryModel categoryModel)
        {
            await _categoryRepository.AddAsync(new Category()
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name
            });

            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Created, categoryModel);
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpPut("{id}")]
        public async Task<ApiResult> EditCategory([FromRoute] Guid id, [FromBody] CategoryModel categoryModel)
        {
            Category category = new Category()
            {
                Id = id,
                Name = categoryModel.Name
            };

            _categoryRepository.Update(category);

            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Ok, new CategoryModel() { Id = id, Name = categoryModel.Name });
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult> DeleteCategory([FromRoute] Guid id)
        {
            _categoryRepository.Delete(id);
            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Ok);
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpGet("{id}")]
        public async Task<ApiResult> CategoryDetails([FromRoute] Guid id)
        {
            Category? category = await _categoryRepository.FindByIdAsync(id);

            if (category != null)
            {
                CategoryModel categoryModel = new CategoryModel()
                {
                    Id = category.Id,
                    Name = category.Name
                };

                return new ApiResult(Status.Ok, categoryModel);
            }

            return new ApiResult(Status.NotFound);
        }
    }
}
