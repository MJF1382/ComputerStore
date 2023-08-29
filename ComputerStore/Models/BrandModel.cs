using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class BrandModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "نام برند را وارد کنید.")]
        [StringLength(100, ErrorMessage = "نام برند باید حداکثر 100 کاراکتر باشد.")]
        public string Name { get; set; }
    }
}
