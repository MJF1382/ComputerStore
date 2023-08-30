using ComputerStore.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Database.IdentityStores
{
    public class ComputerStoreUserStore : UserStore<AppUser, AppRole, ComputerStoreDbContext, Guid, AppUserClaim, AppUserRole, AppUserLogin, AppUserToken, AppRoleClaim>, IComputerStoreUserStore
    {
        public ComputerStoreUserStore(
            ComputerStoreDbContext context,
            IdentityErrorDescriber describer = null)
            : base(context, describer)
        {

        }
    }
}
