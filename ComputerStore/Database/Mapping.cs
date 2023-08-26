using ComputerStore.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerStore.Database
{
    public static class Mapping
    {
        public static void MapIds(this ModelBuilder modelBuilder)
        {
            var d = modelBuilder.Entity<Product>();

            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Article>().HasKey(p => p.Id);
            modelBuilder.Entity<Category>().HasKey(p => p.Id);
            modelBuilder.Entity<Brand>().HasKey(p => p.Id);
            modelBuilder.Entity<ExtraDetail>().HasKey(p => p.Id);
            modelBuilder.Entity<Ticket>().HasKey(p => p.Id);
            modelBuilder.Entity<Comment>().HasKey(p => p.Id);
            modelBuilder.Entity<Purchase>().HasKey(p => p.Id);
            modelBuilder.Entity<ProductPurchase>().HasKey(p => new { p.ProductId, p.PurchaseId });
            modelBuilder.Entity<ArticleTag>().HasKey(p => new { p.ArticleId, p.TagId });
        }

        public static void MapProduct(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.BrandId);
        }

        public static void MapArticle(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Articles)
                .HasForeignKey(p => p.CategoryId);
        }

        public static void MapExtraDetail(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExtraDetail>()
                .HasOne(p => p.Product)
                .WithMany(p => p.ExtraDetails)
                .HasForeignKey(p => p.ProductId);
        }

        public static void MapComment(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<Comment>()
                .HasOne(p => p.Article)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.ArticleId);
        }

        public static void MapArticleTag(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleTag>()
                .HasOne(p => p.Article)
                .WithMany(p => p.ArticleTags)
                .HasForeignKey(p => p.ArticleId);

            modelBuilder.Entity<ArticleTag>()
                .HasOne(p => p.Tag)
                .WithMany(p => p.ArticleTags)
                .HasForeignKey(p => p.TagId);
        }
    }
}
