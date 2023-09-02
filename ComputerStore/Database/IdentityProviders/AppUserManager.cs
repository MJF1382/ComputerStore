using ComputerStore.Database.Entities;
using ComputerStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text;

namespace ComputerStore.Database.IdentityProviders
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(
            IUserStore<AppUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<AppUser> passwordHasher,
            IEnumerable<IUserValidator<AppUser>> userValidators,
            IEnumerable<IPasswordValidator<AppUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<AppUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

        }

        public async Task<List<UserModel>> GetUsersWithRolesAsync()
        {
            var users = await Users.Include(p => p.UserRoles).ThenInclude(p => p.Role).ToListAsync();
            List<UserModel> userModels = users.Select<AppUser, UserModel>(user => user).ToList();

            for (int i = 0; i < users.Count; i++)
            {
                userModels[i].RoleIds = users[i].UserRoles.Select(userRole => userRole.Role.Name).ToList();
            }

            return userModels;
        }

        public async Task<IdentityResult> RemoveAllRolesAsync(AppUser user)
        {
            List<string> roles = (await GetRolesAsync(user)).ToList();

            return await RemoveFromRolesAsync(user, roles);
        }
    }
}
