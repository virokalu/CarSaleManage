using Microsoft.AspNetCore.Identity;

namespace CarSaleManage.Models.Services
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> ListAsync();
        Task<IdentityResult> GetAsync(string id);
        Task<IdentityResult> CreateAsync(AppUser user);
        Task<IdentityResult> UpdateAsync(AppUser user);
        Task<IdentityResult> DeleteAsync(string userId);
    }
}
