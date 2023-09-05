using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.Repositories;
using ComputerStore.Database.UnitOfWork;
using ComputerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/admin/categories")]
    [ApiController]
    public class AdminCategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Category> _categoryRepository;

        public AdminCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = _unitOfWork.RepositoryBase<Category>();
        }

        [HttpGet]
        public async Task<ApiResult> GetCategories()
        {
            List<CategoryModel> categories = (await _categoryRepository.GetAllAsync()).Select<Category, CategoryModel>(category => category).ToList();

            return new ApiResult(Status.Ok, categories);
        }

        [HttpPost]
        public async Task<ApiResult> AddCategory([FromBody] CategoryModel categoryModel)
        {
            categoryModel.Id = Guid.NewGuid();

            await _categoryRepository.AddAsync(categoryModel);
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
            categoryModel.Id = id;

            _categoryRepository.Update(categoryModel);
            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Ok, categoryModel);
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
                CategoryModel categoryModel = category;

                return new ApiResult(Status.Ok, categoryModel);
            }

            return new ApiResult(Status.NotFound);
        }
    }
}
