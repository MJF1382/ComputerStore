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
    }
}
