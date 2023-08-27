using Microsoft.EntityFrameworkCore;
using ComputerStore.Database.Entities;
using ComputerStore.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ComputerStore.Database
{
    public class ComputerStoreDbContext : IdentityDbContext<AppUser, IdentityRole<string>, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ComputerStoreDbContext(DbContextOptions<ComputerStoreDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .ToTable("AppUsers");

            modelBuilder.Entity<IdentityRole<string>>()
                .ToTable("AppRoles");

            modelBuilder.Entity<IdentityUserClaim<string>>()
                .ToTable("AppUserClaims");

            modelBuilder.Entity<IdentityRoleClaim<string>>()
                .ToTable("AppRoleClaims");

            modelBuilder.Entity<IdentityUserRole<string>>()
                .ToTable("AppUserRoles");

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .ToTable("AppUserLogins");

            modelBuilder.Entity<IdentityUserToken<string>>()
                .ToTable("AppUserTokens");

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
