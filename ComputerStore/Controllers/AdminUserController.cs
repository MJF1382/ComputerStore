using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.IdentityProviders;
using ComputerStore.Database.UnitOfWork;
using ComputerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/admin/users")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly AppUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AdminUserController(AppUserManager userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ApiResult> GetUsers()
        {
            return new ApiResult(Status.Ok, await _userManager.GetUsersWithRolesAsync());
        }

        [HttpPost]
        public async Task<ApiResult> AddUser([FromBody] UserModel userModel)
        {
            List<string> errors = new List<string>();
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
                    else
                    {
                        await _userManager.DeleteAsync(user);

                        foreach (string errorMessage in result.Errors.Select(identityError => identityError.Description))
                        {
                            errors.Add(errorMessage);
                        }
                    }
                }
                else
                {
                    errors.Add("نقش های کاربر را وارد کنید.");

                    return new ApiResult(Status.BadRequest, null, errors);
                }
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
        public async Task<ApiResult> EditUser([FromRoute] Guid id, [FromBody] UserModel userModel)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                List<string> errors = new List<string>();

                userModel.Id = id;
                userModel.UserName = userModel.Email;

                user.FullName = userModel.FullName;
                user.BirthDay = userModel.BirthDay;
                user.Email = userModel.Email;
                user.UserName = userModel.Email;
                user.PhoneNumber = userModel.PhoneNumber;
                user.EmailConfirmed = userModel.EmailConfirmed;
                user.PhoneNumberConfirmed = userModel.PhoneNumberConfirmed;
                user.TwoFactorEnabled = userModel.TwoFactorEnabled;
                user.LockoutEnd = userModel.LockoutEnd;
                user.LockoutEnabled = userModel.LockoutEnabled;
                user.AccessFailedCount = userModel.AccessFailedCount;

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await _unitOfWork.BeginTransactionAsync();

                    try
                    {
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, userModel.Password);

                        string errorMessage = "";

                        _unitOfWork.CommitTransaction(out errorMessage);

                        if (errorMessage.HasValue())
                        {
                            errors.Add(errorMessage);
                            return new ApiResult(Status.InternalServerError, null, errors);
                        }
                    }
                    catch
                    {
                        await _unitOfWork.RollBackAsync();
                        errors.Add("خطا در اتصال به سرور، لطفا بعدا دوباره امتحان کنید.");
                    }

                    result = await _userManager.RemoveAllRolesAsync(user);

                    if (result.Succeeded)
                    {
                        if (userModel.RoleIds.Count > 0)
                        {
                            result = await _userManager.AddToRolesAsync(user, userModel.RoleIds);

                            if (result.Succeeded)
                            {
                                List<string> roleIds = userModel.RoleIds;

                                userModel = user;
                                userModel.RoleIds = roleIds;

                                return new ApiResult(Status.Ok, userModel);
                            }
                            else
                            {
                                foreach (string errorMessage in result.Errors.Select(identityError => identityError.Description))
                                {
                                    errors.Add(errorMessage);
                                }
                            }
                        }
                        else
                        {
                            return new ApiResult(Status.Ok, userModel);
                        }
                    }
                    else
                    {
                        foreach (string errorMessage in result.Errors.Select(identityError => identityError.Description))
                        {
                            errors.Add(errorMessage);
                        }
                    }
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
        public async Task<ApiResult> DeleteUser([FromRoute] Guid id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                List<string> errors = new List<string>();
                IdentityResult result = await _userManager.DeleteAsync(user);

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
            }

            return new ApiResult(Status.NotFound);
        }

        [HttpGet("{id}")]
        public async Task<ApiResult> UserDetails([FromRoute] Guid id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                UserModel userModel = user;
                userModel.RoleIds = (await _userManager.GetRolesAsync(user)).ToList();

                return new ApiResult(Status.Ok, userModel);
            }

            return new ApiResult(Status.NotFound);
        }
    }
}
