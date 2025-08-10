using CarSaleManage.Models;
using CarSaleManage.Models.Dtos;
using CarSaleManage.Models.Services;
using CarSaleManage.Models.Services.Communication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarSaleManage.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;

        public UserService(UserManager<AppUser> userManager, IUserStore<AppUser> userStore)
        {
            this._userManager = userManager;
            this._userStore = userStore;
        }

        public async Task<IdentityResult> CreateAsync(UserDto user)
        {
            var newUser = new AppUser
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
            };
            await _userStore.SetUserNameAsync(newUser, user.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(newUser);

            if (result.Succeeded)
            {
                var roleresult = await _userManager.AddToRoleAsync(newUser, Data.Enums.Roles.User.ToString());
                if (roleresult.Succeeded) return IdentityResult.Success;
                return IdentityResult.Failed((IdentityError)roleresult.Errors);
            }
            return IdentityResult.Failed((IdentityError)result.Errors);
        }

        public async Task<IdentityResult> DeleteAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = $"No user found."
                });
            }
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed((IdentityError)result.Errors);
        }

        public async Task<ServiceResult<AppUser>> GetAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return ServiceResult<AppUser>.Fail("No user found.");
            }
            return ServiceResult<AppUser>.Ok(user);
        }

        public async Task<IEnumerable<AppUser>> ListAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> SearchListAsync(string? searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return await _userManager.Users.ToListAsync();
            }
            else
            {
                return await _userManager.Users
                .Where(u => u.Firstname.Contains(searchString) || u.Lastname.Contains(searchString) || (u.Email != null && u.Email.Contains(searchString)))
                .ToListAsync();
            }
        }

        public async Task<IdentityResult> UpdateAsync(UserEditDto user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = $"No user found with ID {user.Id}."

                });
            }
            if (existingUser.PhoneNumber != user.PhoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(existingUser, user.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "Error",
                        Description = $"Unexpected error when trying to set phone number."
                    });
                }
            }
            if (existingUser.Firstname != user.Firstname || existingUser.Lastname != user.Lastname)
            {
                existingUser.Firstname = user.Firstname;
                existingUser.Lastname = user.Lastname;
                var setNameResult = await _userManager.UpdateAsync(existingUser);
                if (!setNameResult.Succeeded)
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "Error",
                        Description = $"Unexpected error when trying to set name."
                    });
                }
            }

            return IdentityResult.Success;
        }
    }
}
