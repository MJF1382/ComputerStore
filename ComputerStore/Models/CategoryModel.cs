using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "نام دسته بندی را وارد کنید.")]
        [StringLength(100, ErrorMessage = "نام دسته بندی باید حداکثر 100 کاراکتر باشد.")]
        public string Name { get; set; }
    }
}
