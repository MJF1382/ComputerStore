using ComputerStore.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class BrandModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "نام برند را وارد کنید.")]
        [StringLength(100, ErrorMessage = "نام برند باید حداکثر 100 کاراکتر باشد.")]
        public string Name { get; set; }

        public static implicit operator BrandModel(Brand brand)
        {
            return new BrandModel()
            {
                Id = brand.Id,
                Name = brand.Name
            };
        }

        public static implicit operator Brand(BrandModel brandModel)
        {
            return new Brand()
            {
                Id = brandModel.Id,
                Name = brandModel.Name
            };
        }
    }
}
