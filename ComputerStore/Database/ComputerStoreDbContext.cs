using Microsoft.EntityFrameworkCore;
using ComputerStore.Database.Entities;
using ComputerStore.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ComputerStore.Database
{
    public class ComputerStoreDbContext : IdentityDbContext<AppUser>
    {
        public ComputerStoreDbContext(DbContextOptions<ComputerStoreDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.MapIds();
            modelBuilder.MapProduct();
            modelBuilder.MapArticle();
            modelBuilder.MapExtraDetail();
            modelBuilder.MapComment();
            modelBuilder.MapArticleTag();
            modelBuilder.MapProductPurchase();

            modelBuilder.Entity<AppUser>()
                .ToTable("AppUsers");

            modelBuilder.Entity<AppUser>()
                .ToTable("AppRoles");

            modelBuilder.Entity<AppUser>()
                .ToTable("AppUserClaims");

            modelBuilder.Entity<AppUser>()
                .ToTable("AppRoleClaims");

            modelBuilder.Entity<AppUser>()
                .ToTable("AppUserRoles");

            modelBuilder.Entity<AppUser>()
                .ToTable("AppUserLogins");

            modelBuilder.Entity<AppUser>()
                .ToTable("AppUserTokens");
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
