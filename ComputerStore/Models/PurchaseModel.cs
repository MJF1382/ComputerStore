using ComputerStore.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class PurchaseModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "خریدار را وارد کنید.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "نام و نام خانوادگی خود را وارد کنید.")]
        [StringLength(100, ErrorMessage = "عنوان محصول یاید حداکثر 100 کاراکتر باشد.")]
        public string FullName { get; set; }

        [StringLength(100, ErrorMessage = "نام شرکت یاید حداکثر 100 کاراکتر باشد.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "آدرس خود را وارد کنید.")]
        [StringLength(100, ErrorMessage = "آدرس یاید حداکثر 100 کاراکتر باشد.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "شهر محل زندگی خود را وارد کنید.")]
        [StringLength(100, ErrorMessage = "شهر محل زندگی یاید حداکثر 100 کاراکتر باشد.")]
        public string City { get; set; }

        [Required(ErrorMessage = "استان محل زندگی خود را وارد کنید.")]
        [StringLength(100, ErrorMessage = "استان محل زندگی یاید حداکثر 100 کاراکتر باشد.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "کد پستی خود را وارد کنید.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "کد پستی یاید 10 کاراکتر باشد.")]
        public string PostalCode { get; set; }

        [StringLength(300, ErrorMessage = "توضیحات خرید یاید حداکثر 300 کاراکتر باشد.")]
        public string? Description { get; set; }

        public int Amount { get; set; }

        public DateTime PurchaseDateTime { get; set; }

        [Required(ErrorMessage = "هیچ محصولی در سبد خرید شما وجود ندارد.")]
        public List<Guid> ProductIds { get; set; }

        public static implicit operator PurchaseModel(Purchase purchase)
        {
            return new PurchaseModel()
            {
                Address = purchase.Address,
                Amount = purchase.Amount,
                City = purchase.City,
                CompanyName = purchase.CompanyName,
                Description = purchase.Description,
                FullName = purchase.FullName,
                Id = purchase.Id,
                PostalCode = purchase.PostalCode,
                Province = purchase.Province,
                PurchaseDateTime = purchase.PurchaseDateTime,
                UserId = purchase.UserId
            };
        }

        public static implicit operator Purchase(PurchaseModel purchase)
        {
            return new Purchase()
            {
                Address = purchase.Address,
                Amount = purchase.Amount,
                City = purchase.City,
                CompanyName = purchase.CompanyName,
                Description = purchase.Description,
                FullName = purchase.FullName,
                Id = purchase.Id,
                PostalCode = purchase.PostalCode,
                Province = purchase.Province,
                PurchaseDateTime = purchase.PurchaseDateTime,
                UserId = purchase.UserId
            };
        }
    }
}
