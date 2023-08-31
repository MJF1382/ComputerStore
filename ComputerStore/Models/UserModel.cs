using ComputerStore.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "نام و نام خانوادگی خود را وارد کنید.")]
        [StringLength(100, ErrorMessage = "نام و نام خانوادگی باید حداکثر 100 کاراکتر باشد.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "تاریخ تولد خود را وارد کنید.")]
        public DateTime BirthDay { get; set; }

        public string? UserName { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "ایمیل خود را وارد کنید.")]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public List<string> RoleIds { get; set; }

        public static implicit operator UserModel(AppUser user)
        {
            return new UserModel()
            {
                AccessFailedCount = user.AccessFailedCount,
                BirthDay = user.BirthDay,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                FullName = user.FullName,
                Id = user.Id,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName
            };
        }

        public static implicit operator AppUser(UserModel userModel)
        {
            return new AppUser()
            {
                AccessFailedCount = userModel.AccessFailedCount,
                BirthDay = userModel.BirthDay,
                Email = userModel.Email,
                EmailConfirmed = userModel.EmailConfirmed,
                FullName = userModel.FullName,
                Id = userModel.Id,
                LockoutEnabled = userModel.LockoutEnabled,
                LockoutEnd = userModel.LockoutEnd,
                PhoneNumber = userModel.PhoneNumber,
                PhoneNumberConfirmed = userModel.PhoneNumberConfirmed,
                TwoFactorEnabled = userModel.TwoFactorEnabled,
                UserName = userModel.UserName
            };
        }
    }
}
