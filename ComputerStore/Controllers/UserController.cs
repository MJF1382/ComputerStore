﻿using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.IdentityProviders;
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
        private readonly AppUserManager _userManager;

        public UserController(AppUserManager userManager)
        {
            _userManager = userManager;
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
    }
}
