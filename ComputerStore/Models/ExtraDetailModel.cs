using ComputerStore.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class ExtraDetailModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        [Required]
        [StringLength(300)]
        public string Value { get; set; }

        public static implicit operator ExtraDetailModel(ExtraDetail extraDetail)
        {
            return new ExtraDetailModel()
            {
                Id = extraDetail.Id,
                Name = extraDetail.Name,
                ProductId = extraDetail.ProductId,
                Value = extraDetail.Value
            };
        }

        public static implicit operator ExtraDetail(ExtraDetailModel extraDetailModel)
        {
            return new ExtraDetail()
            {
                Id = extraDetailModel.Id,
                Name = extraDetailModel.Name,
                ProductId = extraDetailModel.ProductId,
                Value = extraDetailModel.Value
            };
        }
    }
}
