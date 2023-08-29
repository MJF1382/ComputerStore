﻿using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "عنوان محصول را وارد کنید.")]
        [StringLength(100, ErrorMessage = "عنوان محصول یاید حداکثر 100 کاراکتر باشد.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "دسته بندی محصول را وارد کنید.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "برند محصول را وارد کنید.")]
        public Guid BrandId { get; set; }

        [Required(ErrorMessage = "خلاصه محصول را وارد کنید.")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "گارانتی محصول را وارد کنید.")]
        [StringLength(100, ErrorMessage = "گارانتی محصول یاید حداکثر 100 کاراکتر باشد.")]
        public string Warranty { get; set; }

        [Required(ErrorMessage = "قیمت محصول را وارد کنید.")]
        public int Price { get; set; }

        public int? OfferedPrice { get; set; }

        [Required(ErrorMessage = "تعداد محصول را وارد کنید.")]
        public int Quantity { get; set; }
    }
}