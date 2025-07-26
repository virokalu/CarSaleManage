using CarSaleManage.Models.Services.Communication;

namespace CarSaleManage.Models.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> ListAsync();
        Task<ServiceResult<Vehicle>> FindByIdAsync(int id);
        Task<ServiceResult<Vehicle>> Save(Vehicle vehicle);
        Task<ServiceResult<Vehicle>> Update(Vehicle vehicle);
        Task<ServiceResult<Vehicle>> Delete(int id);
    }
}
