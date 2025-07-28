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
                await _userManager.AddToRoleAsync(newUser, Data.Enums.Roles.User.ToString());
                return IdentityResult.Success;
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
            await _userManager.DeleteAsync(user);
            return IdentityResult.Success;
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

        public async Task<IdentityResult> UpdateAsync(UserEditDto user)
        {
            var updateUser = new AppUser
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                PhoneNumber = user.PhoneNumber,
            };
            var exisitingUser = await _userManager.UpdateAsync(updateUser);
            if (exisitingUser == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = $"No user found."
                });
            }
            return IdentityResult.Success;
        }
    }
}
