using ComputerStore.Database.Entities;
using System.ComponentModel.DataAnnotations;

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

        public CategoryModel? Category { get; set; }
        public BrandModel? Brand { get; set; }
        public List<ExtraDetailModel>? ExtraDetails { get; set; }
        public List<CommentModel>? Comments { get; set; }

        public static implicit operator ProductModel(Product product)
        {
            ProductModel productModel = new ProductModel()
            {
                Id = product.Id,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                OfferedPrice = product.OfferedPrice,
                Price = product.Price,
                Quantity = product.Quantity,
                Summary = product.Summary,
                Title = product.Title,
                Warranty = product.Warranty
            };

            if (product.Brand != null)
            {
                productModel.Brand = product.Brand;
            }
            if (product.Category != null)
            {
                productModel.Category = product.Category;
            }
            if (product.ExtraDetails != null)
            {
                productModel.ExtraDetails = product.ExtraDetails.Select<ExtraDetail, ExtraDetailModel>(extraDetail => extraDetail).ToList();
            }
            if (product.Comments != null)
            {
                productModel.Comments = product.Comments.Select<Comment, CommentModel>(comment => comment).ToList();
            }
            
            return productModel;
        }

        public static implicit operator Product(ProductModel productModel)
        {
            Product product = new Product()
            {
                Id = productModel.Id,
                BrandId = productModel.BrandId,
                CategoryId = productModel.CategoryId,
                OfferedPrice = productModel.OfferedPrice,
                Price = productModel.Price,
                Quantity = productModel.Quantity,
                Summary = productModel.Summary,
                Title = productModel.Title,
                Warranty = productModel.Warranty
            };

            if (productModel.Brand != null)
            {
                product.Brand = productModel.Brand;
            }
            if (productModel.Category != null)
            {
                product.Category = productModel.Category;
            }
            if (productModel.ExtraDetails != null)
            {
                product.ExtraDetails = productModel.ExtraDetails.Select<ExtraDetailModel, ExtraDetail>(extraDetail => extraDetail).ToList();
            }
            if (productModel.Comments != null)
            {
                product.Comments = productModel.Comments.Select<CommentModel, Comment>(comment => comment).ToList();
            }

            return product;
        }
    }
}
