using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ApiResult> GetRoles()
        {
            List<RoleModel> roles = await _roleManager.Roles.Select<AppRole, RoleModel>(role => role).ToListAsync();

            return new ApiResult(Status.Ok, roles);
        }
    }
}
