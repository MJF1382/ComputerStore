using Microsoft.EntityFrameworkCore;
using ComputerStore.Database.Entities;
using ComputerStore.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ComputerStore.Database
{
    public class ComputerStoreDbContext : IdentityDbContext<AppUser, AppRole, string, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public ComputerStoreDbContext(DbContextOptions<ComputerStoreDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.MapDefaultIdentity();
            modelBuilder.MapIdentityTableNames();

            modelBuilder.MapIds();
            modelBuilder.MapProduct();
            modelBuilder.MapArticle();
            modelBuilder.MapExtraDetail();
            modelBuilder.MapComment();
            modelBuilder.MapArticleTag();
            modelBuilder.MapProductPurchase();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ExtraDetail> ExtraDetails { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<ProductPurchase> ProductPurchases { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
    }
}
