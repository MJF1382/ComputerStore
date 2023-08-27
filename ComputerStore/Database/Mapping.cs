using ComputerStore.Database.Entities;
using Microsoft.AspNetCore.Identity;
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

            modelBuilder.Entity<Article>()
                .HasOne(p => p.User)
                .WithMany(p => p.Articles)
                .HasForeignKey(p => p.UserId);
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
                .HasForeignKey(p => p.ProductId)
                .IsRequired(false); ;

            modelBuilder.Entity<Comment>()
                .HasOne(p => p.Article)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.ArticleId)
                .IsRequired(false);

            modelBuilder.Entity<Comment>()
                .HasOne(p => p.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.UserId);
        }

        public static void MapTicket(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne(p => p.User)
                .WithMany(p => p.Tickets)
                .HasForeignKey(p => p.UserId);
        }

        public static void MapPurchase(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.User)
                .WithMany(p => p.Purchases)
                .HasForeignKey(p => p.UserId);
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

        public static void MapProductPurchase(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductPurchase>()
                .HasOne(p => p.Product)
                .WithMany(p => p.ProductPurchases)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<ProductPurchase>()
                .HasOne(p => p.Purchase)
                .WithMany(p => p.ProductPurchases)
                .HasForeignKey(p => p.PurchaseId);
        }


        public static void MapDefaultIdentity(this ModelBuilder modelBuilder)
        {
            // User

            modelBuilder.Entity<AppUser>()
                .HasMany(p => p.Claims)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .IsRequired();

            modelBuilder.Entity<AppUser>()
                .HasMany(p => p.Logins)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .IsRequired();

            modelBuilder.Entity<AppUser>()
                .HasMany(p => p.Tokens)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .IsRequired();

            modelBuilder.Entity<AppUser>()
                .HasMany(p => p.UserRoles)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .IsRequired();

            // Role

            modelBuilder.Entity<AppRole>()
                .HasMany(p => p.UserRoles)
                .WithOne(p => p.Role)
                .HasForeignKey(p => p.RoleId)
                .IsRequired();

            modelBuilder.Entity<AppRole>()
                .HasMany(p => p.RoleClaims)
                .WithOne(p => p.Role)
                .HasForeignKey(p => p.RoleId)
                .IsRequired();
        }

        public static void MapIdentityTableNames(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .ToTable("AppUsers");

            modelBuilder.Entity<AppRole>()
                .ToTable("AppRoles");

            modelBuilder.Entity<AppUserClaim>()
                .ToTable("AppUserClaims");

            modelBuilder.Entity<AppRoleClaim>()
                .ToTable("AppRoleClaims");

            modelBuilder.Entity<AppUserRole>()
                .ToTable("AppUserRoles");

            modelBuilder.Entity<AppUserLogin>()
                .ToTable("AppUserLogins");

            modelBuilder.Entity<AppUserToken>()
                .ToTable("AppUserTokens");
        }
    }
}
