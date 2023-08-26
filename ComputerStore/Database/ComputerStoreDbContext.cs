using Microsoft.EntityFrameworkCore;
using ComputerStore.Database.Entities;
using ComputerStore.Database;

namespace ComputerStore.Database
{
    public class ComputerStoreDbContext : DbContext
    {
        public ComputerStoreDbContext(DbContextOptions<ComputerStoreDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<ExtraDetail> ExtraDetails { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<ProductPurchase> ProductPurchases { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
    }
}
