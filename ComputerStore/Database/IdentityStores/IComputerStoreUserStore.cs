using ComputerStore.Database.Entities;
using ComputerStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Database.IdentityStores
{
    public interface IComputerStoreUserStore : IUserStore<AppUser>
    {
        Task<List<UserModel>> GetUsersWithRolesAsync();
    }
}
