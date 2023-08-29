using ComputerStore.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "نام دسته بندی را وارد کنید.")]
        [StringLength(100, ErrorMessage = "نام دسته بندی باید حداکثر 100 کاراکتر باشد.")]
        public string Name { get; set; }

        public static implicit operator CategoryModel(Category category)
        {
            return new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static implicit operator Category(CategoryModel categoryModel)
        {
            return new Category()
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name
            };
        }
    }
}
