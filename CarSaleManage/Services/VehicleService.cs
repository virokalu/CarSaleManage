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
                await _vehicleRepository.CompleteAsync();
                return ServiceResult<Vehicle>.Ok();
            }

        }

        public async Task<ServiceResult<Vehicle>> FindByIdAsync(int id)
        {
            var vehicle = await _vehicleRepository.FindByIdAsync(id);
            if (vehicle == null)
            {
                return ServiceResult<Vehicle>.Fail("Vehicle not Found");
            }
            else
            {
                return ServiceResult<Vehicle>.Ok();
            }
        }

        public async Task<IEnumerable<Vehicle>> ListAsync()
        {
            return await _vehicleRepository.ListAsync();
        }

        public async Task<ServiceResult<Vehicle>> Save(Vehicle vehicle, List<IFormFile> Images)
        {
            var imagePaths = new List<string>();
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
            foreach (var formFile in Images)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    // Store the relative path to the image
                    imagePaths.Add("/uploads/" + fileName);
                }
            }
            vehicle.Images = imagePaths;


        }

        public Task<ServiceResult<Vehicle>> Update(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
