using ComputerStore.Database.Entities;
using ComputerStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Database.IdentityStores
{
    public class ComputerStoreUserStore : UserStore<AppUser, AppRole, ComputerStoreDbContext, Guid, AppUserClaim, AppUserRole, AppUserLogin, AppUserToken, AppRoleClaim>, IComputerStoreUserStore
    {
        public ComputerStoreUserStore(ComputerStoreDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
            AutoSaveChanges = false;
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
    }
}
