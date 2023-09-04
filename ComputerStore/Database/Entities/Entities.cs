using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Database.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid BrandId { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        [StringLength(100)]
        public string Warranty { get; set; }

        [Required]
        public int Price { get; set; }

        public int? OfferedPrice { get; set; }

        [Required]
        public int Quantity { get; set; }


        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public List<ProductPurchase> ProductPurchases { get; set; }
        public List<ExtraDetail> ExtraDetails { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class ProductPurchase
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid PurchaseId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Product Product { get; set; }
        public Purchase Purchase { get; set; }
    }

    public class Article
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime PublishDateTime { get; set; }

        public Category Category { get; set; }
        public AppUser User { get; set; }
        public List<ArticleTag> ArticleTags { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class ArticleTag
    {
        [Required]
        public Guid ArticleId { get; set; }

        [Required]
        public Guid TagId { get; set; }

        public Article Article { get; set; }
        public Tag Tag { get; set; }
    }

    public class Category
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
        public List<Article> Articles { get; set; }
    }

    public class Brand
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }

    public class ExtraDetail
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

        public Product Product { get; set; }
    }

    public class Ticket
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        [StringLength(1000)]
        public string Body { get; set; }

        public AppUser User { get; set; }
    }

    public class Comment
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }


        public Guid? ProductId { get; set; }


        public Guid? ArticleId { get; set; }

        [Required]
        public float Score { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [StringLength(1000)]
        public string Body { get; set; }

        [Required]
        public DateTime PublishDateTime { get; set; }

        public Product Product { get; set; }
        public Article Article { get; set; }
        public AppUser User { get; set; }
    }

    public class Purchase
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [StringLength(100)]
        public string Province { get; set; }

        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public DateTime PurchaseDateTime { get; set; }

        public List<ProductPurchase> ProductPurchases { get; set; }
        public AppUser User { get; set; }
    }

    public class Tag
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<ArticleTag> ArticleTags { get; set; }
    }

    public class Satisfaction
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public float Score { get; set; }

        [Required]
        [StringLength(300)]
        public string Body { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        public AppUser User { get; set; }
    }
}
