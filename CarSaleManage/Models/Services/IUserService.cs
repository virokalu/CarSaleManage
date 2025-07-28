using CarSaleManage.Models.Dtos;
using CarSaleManage.Models.Services.Communication;
using Microsoft.AspNetCore.Identity;

namespace CarSaleManage.Models.Services
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> ListAsync();
        Task<ServiceResult<AppUser>> GetAsync(string id);
        Task<IdentityResult> CreateAsync(UserDto user);
        Task<IdentityResult> UpdateAsync(UserEditDto user);
        Task<IdentityResult> DeleteAsync(string userId);
    }
}
