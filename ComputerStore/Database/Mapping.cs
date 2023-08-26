using ComputerStore.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Database
{
    public static class Mapping
    {
        public static void MapIds(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Article>().HasKey(p => p.Id);
            modelBuilder.Entity<Category>().HasKey(p => p.Id);
            modelBuilder.Entity<Brands>().HasKey(p => p.Id);
            modelBuilder.Entity<ExtraDetail>().HasKey(p => p.Id);
            modelBuilder.Entity<Ticket>().HasKey(p => p.Id);
            modelBuilder.Entity<Comment>().HasKey(p => p.Id);
            modelBuilder.Entity<Purchase>().HasKey(p => p.Id);
            modelBuilder.Entity<ProductPurchase>().HasKey(p => new { p.ProductId, p.PurchaseId });
            modelBuilder.Entity<ArticleTag>().HasKey(p => new { p.ArticleId, p.TagId });
        }
    }
}
