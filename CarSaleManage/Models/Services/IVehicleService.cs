using CarSaleManage.Models.Dtos;
using CarSaleManage.Models.Services.Communication;

namespace CarSaleManage.Models.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> ListAsync();
        Task<ServiceResult<Vehicle>> FindByIdAsync(int id);
        Task<ServiceResult<Vehicle>> Save(VehicleDto vehicleDto);
        Task<ServiceResult<Vehicle>> Update(VehicleEditDto vehicleEditDto);
        Task<ServiceResult<Vehicle>> Delete(int id);
        Task<ServiceResult<Vehicle>> Buy(int id, string userId);
        Task<ServiceResult<Vehicle>> RemoveBuy(int id);
    }
}
