using CarSaleManage.Models;
using CarSaleManage.Models.Repositories;
using CarSaleManage.Models.Services;
using CarSaleManage.Models.Services.Communication;

namespace CarSaleManage.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ServiceResult<Vehicle>> Delete(int id)
        {
            var vehicle = await _vehicleRepository.FindByIdAsync(id);
            if (vehicle == null)
            {
                return ServiceResult<Vehicle>.Fail("Vehicle not Found");
            }
            else
            {
                _vehicleRepository.Remove(vehicle);
                _vehicleRepository.CompleteAsync();
            }

        }

        public Task<ServiceResult<Vehicle>> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vehicle>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<Vehicle>> Save(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<Vehicle>> Update(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
