using Habit.Core.RepositoryContract;
using Habit.Repository.Data;
using Habit.Repository.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ReHabit.Habit.Core.Models;
using System;
namespace Habit.Repository.Repository
{
    public class UserRepository 
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(UserManager<User> userManage,
                             RoleManager<IdentityRole> roleManager)
        { 
            _roleManager = roleManager;
            _userManager = userManage;
        }
        public async Task<ResultDto> CreateAsync(User user, string password)
        {
            var isExist = _userManager.FindByEmailAsync(user.Email);
            if (isExist != null)
            {
                new ResultDto
                {
                    Success = false,
                    Message = "User Email Alredy Exist",

                };
                _logger.LogInformation("User Email [{}] is Alredy Exist..", user.Email);
            }

            var result = await _userManager.CreateAsync(user, password);

            if(!result.Succeeded)
            {
                new ResultDto
                {
                    Success = false,
                    Message = "Something Fail for Creating User Account",

                };
            }

            var AddRoleResult = await AddUserToRoleAsync(user, "User");

            if(!AddRoleResult.Success)
            {
                return AddRoleResult;
            }


            return new ResultDto
            {
                Success = true,
                Message = "User Created Successfully",

            };
        }
        private async Task<ResultDto> AddUserToRoleAsync(User user, string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
            }
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return new ResultDto
                {
                    Success = true,
                    Message = $"User '{user.UserName}' added to role '{roleName}'."
                };
            }
            return new ResultDto
            {
                Success = false,
                Message = "Failed to add user to role.",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

    }
}
