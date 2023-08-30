using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.IdentityStores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ComputerStoreUserStore _userStore;

        public UserController(ComputerStoreUserStore userStore)
        {
            _userStore = userStore;
        }
    }
}
