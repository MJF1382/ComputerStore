using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Controllers
{
    [Route("api/admin/roles")]
    [ApiController]
    public class AdminRoleController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;

        public AdminRoleController(RoleManager<AppRole> roleManager)
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
            List<string> errors = new List<string>();
            IdentityResult result = await _roleManager.CreateAsync(roleModel);

            if (result.Succeeded)
            {
                roleModel = await _roleManager.FindByNameAsync(roleModel.Name);

                return new ApiResult(Status.Created, roleModel);
            }
            else
            {
                foreach (string errorMessage in result.Errors.Select(identityError => identityError.Description))
                {
                    errors.Add(errorMessage);
                }
            }

            return new ApiResult(Status.InternalServerError, null, errors);
        }

        [HttpPut("{id}")]
        public async Task<ApiResult> EditRole([FromRoute] Guid id, [FromBody] RoleModel roleModel)
        {
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());

            if (role != null)
            {
                List<string> errors = new List<string>();
                role.Name = roleModel.Name;

                IdentityResult result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    roleModel = role;

                    return new ApiResult(Status.Ok, roleModel);
                }
                else
                {
                    foreach (string errorMessage in result.Errors.Select(identityError => identityError.Description))
                    {
                        errors.Add(errorMessage);
                    }
                }

                return new ApiResult(Status.InternalServerError, null, errors);
            }

            return new ApiResult(Status.NotFound);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult> DeleteResult([FromRoute] Guid id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());

            if (role != null)
            {
                List<string> errors = new List<string>();
                IdentityResult result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return new ApiResult(Status.Ok);
                }
                else
                {
                    foreach (string errorMessage in result.Errors.Select(identityError => identityError.Description))
                    {
                        errors.Add(errorMessage);
                    }
                }

                return new ApiResult(Status.InternalServerError, null, errors);
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
