using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
