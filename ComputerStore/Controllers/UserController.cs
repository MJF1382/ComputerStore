using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.IdentityStores;
using ComputerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IComputerStoreUserStore _userStore;
        private readonly UserManager<AppUser> _userManager;

        public UserController(
            IComputerStoreUserStore userStore,
            UserManager<AppUser> userManager
            )
        {
            _userStore = userStore;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ApiResult> GetUsers()
        {
            return new ApiResult(Status.Ok, await _userStore.GetUsersWithRolesAsync());
        }
    }
}
