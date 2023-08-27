using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Database.Entities
{
    public class AppUser : IdentityUser
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
    }

    public class AppRole : IdentityRole
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<AppRoleClaim> RoleClaims { get; set; }
    }

    public class AppUserRole : IdentityUserRole<string>
    {
        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }

    public class AppUserClaim : IdentityUserClaim<string>
    {
        public virtual AppUser User { get; set; }
    }

    public class AppUserLogin : IdentityUserLogin<string>
    {
        public virtual AppUser User { get; set; }
    }

    public class AppRoleClaim : IdentityRoleClaim<string>
    {
        public virtual AppRole Role { get; set; }
    }

    public class AppUserToken : IdentityUserToken<string>
    {
        public virtual AppUser User { get; set; }
    }
}
