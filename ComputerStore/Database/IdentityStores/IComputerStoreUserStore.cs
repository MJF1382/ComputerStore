using ComputerStore.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace ComputerStore.Database.IdentityStores
{
    public interface IComputerStoreUserStore : IUserStore<AppUser>
    {

    }
}
