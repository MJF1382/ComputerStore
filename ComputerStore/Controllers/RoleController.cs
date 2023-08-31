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

        [HttpPost]
        public async Task<ApiResult> AddRole([FromBody] RoleModel roleModel)
        {
            IdentityResult result = await _roleManager.CreateAsync(roleModel);

            if (result.Succeeded)
            {
                roleModel = await _roleManager.FindByNameAsync(roleModel.Name);

                return new ApiResult(Status.Created, roleModel);
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpPut("{id}")]
        public async Task<ApiResult> EditRole([FromRoute] Guid id, [FromBody] RoleModel roleModel)
        {
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());

            if (role != null)
            {
                role.Name = roleModel.Name;

                IdentityResult result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    roleModel = role;

                    return new ApiResult(Status.Ok, roleModel);
                }

                return new ApiResult(Status.InternalServerError);
            }

            return new ApiResult(Status.NotFound);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult> DeleteResult([FromRoute] Guid id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());

            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return new ApiResult(Status.Ok);
                }

                return new ApiResult(Status.InternalServerError);
            }

            return new ApiResult(Status.NotFound);
        }

        [HttpGet("{id}")]
        public async Task<ApiResult> RoleDetails([FromRoute] Guid id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());

            if (role != null)
            {
                RoleModel roleModel = role;

                return new ApiResult(Status.Ok, roleModel);
            }

            return new ApiResult(Status.NotFound);
        }
    }
}
