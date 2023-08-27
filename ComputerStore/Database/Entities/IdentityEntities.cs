using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Database.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }


        public virtual ICollection<AppUserClaim> Claims { get; set; }
        public virtual ICollection<AppUserLogin> Logins { get; set; }
        public virtual ICollection<AppUserToken> Tokens { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class AppRole : IdentityRole<Guid>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<AppRoleClaim> RoleClaims { get; set; }
    }

    public class AppUserRole : IdentityUserRole<Guid>
    {
        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }

    public class AppUserClaim : IdentityUserClaim<Guid>
    {
        public virtual AppUser User { get; set; }
    }

    public class AppUserLogin : IdentityUserLogin<Guid>
    {
        public virtual AppUser User { get; set; }
    }

    public class AppRoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual AppRole Role { get; set; }
    }

    public class AppUserToken : IdentityUserToken<Guid>
    {
        public virtual AppUser User { get; set; }
    }
}
