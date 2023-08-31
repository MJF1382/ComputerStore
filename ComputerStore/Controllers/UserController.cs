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

        [HttpPost]
        public async Task<ApiResult> AddUser([FromBody] UserModel userModel)
        {
            userModel.UserName = userModel.Email;

            IdentityResult result = await _userManager.CreateAsync(userModel, userModel.Password);

            if (result.Succeeded)
            {
                if (userModel.RoleIds.Count > 0)
                {
                    AppUser user = await _userManager.FindByNameAsync(userModel.UserName);

                    result = await _userManager.AddToRolesAsync(user, userModel.RoleIds);

                    if (result.Succeeded)
                    {
                        userModel = user;

                        return new ApiResult(Status.Created, userModel);
                    }
                }
            }

            return new ApiResult(Status.InternalServerError);
        }
    }
}
